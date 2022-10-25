using BookEF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookEFWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly MyDbContext dbCtx;
        public TestController(MyDbContext dbCtx)
        {
            this.dbCtx = dbCtx;
        }
        [HttpPost]
        public async Task<long> Save()
        {
            dbCtx.Add(new Book { Id = Guid.NewGuid(), Name = "零基础趣学C语言", Price = 59 });
            await dbCtx.SaveChangesAsync();
            return dbCtx.Books.LongCount();
        }
    }
}
