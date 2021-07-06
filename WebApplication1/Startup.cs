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


            //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            services.AddSwaggerGen(c =>
             {
                 c.IncludeXmlComments(Path.Combine(basePath, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
                 c.IgnoreObsoleteProperties();

                 //Bearer 的scheme定义
                 var securityScheme = new OpenApiSecurityScheme()
                 {
                     Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                     Name = "Authorization",
                     //参数添加在头部
                     In = ParameterLocation.Header,
                     //使用Authorize头部
                     Type = SecuritySchemeType.Http,
                     //内容为以 bearer开头
                     Scheme = "bearer",
                     BearerFormat = "JWT"
                 };

                 //把所有方法配置为增加bearer头部信息
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

                 //注册到swagger中
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
            // 启用swagger-ui 中间件，指定 Swagger JSON 终结点，以来公开交互式文档
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API说明文档 V1");
                //c.DefaultModelsExpandDepth(-1);
                c.DocExpansion(DocExpansion.None); // 全部折叠
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
