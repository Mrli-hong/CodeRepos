using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VS2022Demo2;

namespace RedisDemo1.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HelloController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        public HelloController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        [HttpGet]
        public string? GetEnvironment()
        {
            return Environment.GetEnvironmentVariable("haha");
        }
        [HttpGet]
        public async Task<ActionResult<Book?>> GetBooksByID222(int id)
        {
            Book? book = null;  
            string? s = await _distributedCache.GetStringAsync("Book" + id);
            if (s is null)
            {
                Console.WriteLine("缓存中没有去数据库查看了！");
                book = await MyDbContext.GetBookByIDAsync(id);
                await _distributedCache.SetStringAsync("Book" + id, JsonSerializer.Serialize(book));
            }
            else
            {
                Console.WriteLine("缓存中查到了！");
                book = JsonSerializer.Deserialize<Book?>(s);
            }
            if (book == null)
                return NotFound("没找到");
            else
                return book;
        }

    }
}
