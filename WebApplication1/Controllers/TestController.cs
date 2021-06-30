using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npoi.Mapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestDbContext testDbContext;

        public TestController(TestDbContext testDbContext)
        {
            this.testDbContext = testDbContext;
        }
        [HttpPost]
        public async Task<IActionResult> add()
        {
            var entity = new hc_user()
            {
                name = SnowflakeId.Instance.GetId().ToString(),
            };
            testDbContext.Add(entity);
            await testDbContext.SaveChangesAsync();
            await RedisHelper.DelAsync("hc_user1");
            return this.Ok(entity);
        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var aa = await testDbContext.hc_user.ToListAsync();
            var list = await RedisHelper.CacheShellAsync("hc_user1", (int)TimeSpan.FromMinutes(3).TotalSeconds, async () =>
             {
                 return await testDbContext.hc_user.ToListAsync();
             });
            return this.Ok(list);
        }
        [HttpDelete]
        public async Task<IActionResult> del()
        {
            return this.Ok(await RedisHelper.DelAsync("hc_user"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("export")]
        public async Task<IActionResult> export()
        {
            //var mapper = new Mapper("test.xslx");
            await Task.CompletedTask;
            return this.Ok();
        }
    }
}
