using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestDbContext testDbContext;
        private readonly ICache cache;

        public TestController(TestDbContext testDbContext, ICache cache)
        {
            this.testDbContext = testDbContext;
            this.cache = cache;
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
            await cache.DelAsync("hc_user1");
            return this.Ok(entity);
        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var aa = await testDbContext.hc_user.ToListAsync();
            var list = await cache.CacheShellAsync("hc_user1", async () =>
             {
                 return await testDbContext.hc_user.ToListAsync();
             });
            return this.Ok(list);
        }
        [HttpDelete]
        public async Task<IActionResult> del()
        {
            return this.Ok(await cache.DelAsync("hc_user"));
        }
    }
}
