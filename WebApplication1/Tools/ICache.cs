using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace WebApplication1
{
    public interface ICache
    {
        /// <summary>
        /// 添加缓存对象，key 唯一名，key存在时，返回false
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> AddAsync(string key, object value);
        /// <summary>
        /// 添加缓存对象，expireSeconds 过期时间,秒位单位
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        public Task<bool> AddAsync(string key, object value, int expireSeconds = -1);
        /// <summary>
        /// 取得对象，key 唯一名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 添加缓存对象,存在时便会替换，key 唯一名，key存在时，返回true
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> PutAsync(string key, object value);
        /// <summary>
        /// 添加缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        public Task<bool> PutAsync(string key, object value, int expireSeconds = -1);
        public Task<bool> DelAsync(string key);
        public Task<bool> ExistAsync(string key);


        public Task<T> CacheShellAsync<T>(string key, Func<Task<T>> func, int expireSeconds = -1);






    }

    public class InMemoryCache
    {
        private readonly CacheItemPolicy DefaultPolicy = null;
        public InMemoryCache(CacheItemPolicy policy = null)
        {
            this.DefaultPolicy = policy;
        }


        public T GetObject<T>(string key)
        {
            var value = MemoryCache.Default.Get(key);
            if (value == null)
            {
                return default(T);
            }
            return (T)value;
        }
        public void SetObject(string key, object value, int expireSeconds = -1)
        {
            if (expireSeconds != -1)
            {
                var time = DateTimeOffset.Now.AddSeconds(expireSeconds);
                MemoryCache.Default.Set(key, value, time);
            }
            else
                MemoryCache.Default.Set(key, value, this.GetPolicy());
        }


        public async Task<T> GetObjectAsync<T>(string key)
        {
            var value = MemoryCache.Default.Get(key);
            if (value == null)
            {
                return default(T);
            }
            return await Task.FromResult((T)value);
        }
        public async Task SetObjectAsync(string key, object value, int expireSeconds = -1)
        {
            if (expireSeconds != -1)
            {
                var time = DateTimeOffset.Now.AddSeconds(expireSeconds);
                MemoryCache.Default.Set(key, value, time);
            }
            else
                MemoryCache.Default.Set(key, value, this.GetPolicy());
            await Task.CompletedTask;
        }

        public async Task RemoveObjectsAsync(params string[] keys)
        {
            foreach (var key in keys)
            {
                MemoryCache.Default.Remove(key);
            }
            await Task.CompletedTask;
        }

        private CacheItemPolicy GetPolicy()
        {
            if (this.DefaultPolicy != null)
                return this.DefaultPolicy;

            return new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddDays(7),
            };
        }
    }


    public class RedisCache : ICache
    {
        private CSRedisClient redis;
        public RedisCache(CSRedisClient client)
        {
            this.redis = client;
        }

        public Task<bool> AddAsync(string key, object value)
        {
            return this.redis.SetAsync(key, value);
        }

        public async Task<bool> AddAsync(string key, object value, int expireSeconds = -1)
        {
            return await this.redis.SetAsync(key, value, expireSeconds);
        }

        public async Task<T> CacheShellAsync<T>(string key, Func<Task<T>> func, int expireSeconds = -1)
        {
            var aa = await this.redis.CacheShellAsync(key, expireSeconds, func);
            return await this.redis.CacheShellAsync(key, expireSeconds, func);
        }

        public async Task<bool> DelAsync(string key)
        {
            if (await this.redis.ExistsAsync(key))
            {
                try
                {
                    var aa = await this.redis.DelAsync(key);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ExistAsync(string key)
        {
            return await this.redis.ExistsAsync(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await this.redis.GetAsync<T>(key);
        }

        public async Task<bool> PutAsync(string key, object value)
        {
            var existsKey = await this.redis.ExistsAsync(key);
            await this.redis.SetAsync(key, value);
            return existsKey;
        }

        public async Task<bool> PutAsync(string key, object value, int expireSeconds = -1)
        {
            var existsKey = await this.redis.ExistsAsync(key);
            await this.redis.SetAsync(key, value, expireSeconds);
            return existsKey;
        }
    }
}
