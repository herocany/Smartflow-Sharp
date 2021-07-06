using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var connectString = Configuration.GetConnectionString("test");
            services.AddDbContext<TestDbContext>(x => x.UseMySql(connectString, ServerVersion.AutoDetect(connectString)));
            services.AddRedisCache(Configuration);


            //��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            services.AddSwaggerGen(c =>
             {
                 c.IncludeXmlComments(Path.Combine(basePath, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
                 c.IgnoreObsoleteProperties();

                 //Bearer ��scheme����
                 var securityScheme = new OpenApiSecurityScheme()
                 {
                     Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                     Name = "Authorization",
                     //���������ͷ��
                     In = ParameterLocation.Header,
                     //ʹ��Authorizeͷ��
                     Type = SecuritySchemeType.Http,
                     //����Ϊ�� bearer��ͷ
                     Scheme = "bearer",
                     BearerFormat = "JWT"
                 };

                 //�����з�������Ϊ����bearerͷ����Ϣ
                 var securityRequirement = new OpenApiSecurityRequirement
                    {
                        {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "bearerAuth"
                                    }
                                },
                                new string[] {}
                        }
                    };

                 //ע�ᵽswagger��
                 c.AddSecurityDefinition("bearerAuth", securityScheme);
                 c.AddSecurityRequirement(securityRequirement);


                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "test", Version = "v1" });
                 //c.IncludeXmlComments(xmlPath);
                 //c.IgnoreObsoleteProperties();
                 c.ParameterFilter<SwaggerEnumParamFilter>();
                 c.DocumentFilter<SwaggerEnumFilter>();

             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMigrate<TestDbContext>();
            app.UseRouting();

            app.UseSwagger();
            // ����swagger-ui �м����ָ�� Swagger JSON �ս�㣬������������ʽ�ĵ�
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API˵���ĵ� V1");
                //c.DefaultModelsExpandDepth(-1);
                c.DocExpansion(DocExpansion.None); // ȫ���۵�
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
