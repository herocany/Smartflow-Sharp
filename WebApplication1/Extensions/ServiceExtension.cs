﻿
using CSRedis;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Redis;

namespace WebApplication1
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            RedisHelper.Initialization(new CSRedisClient(configuration.GetConnectionString("redis")));
            services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
            return services;
        }
        public static IApplicationBuilder UseMigrate<T>(this IApplicationBuilder builder) where T : DbContext
        {
            using var scope = builder.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<T>();
            context.Database.Migrate();
            return builder;
        }
    }
}
