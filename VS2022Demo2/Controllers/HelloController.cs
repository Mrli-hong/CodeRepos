using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace VS2022Demo2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HelloController : ControllerBase
    {
        private readonly IMemoryCache memory;
        public HelloController(IMemoryCache memory)
        {
            this.memory = memory;
        }

        [HttpGet]
        public string Index()
        {
            return "Heelo";
        }
        [HttpGet]
        public ActionResult<Book?> GetBookByID(int id)
        {
            var result = MyDbContext.GetBookByID(id);
            if (result == null)
                return NotFound("参数不正确！");
            else
            {
                return result;
            }
        }
        [HttpGet]
        public async Task<ActionResult<Book?>> GetBookByIDAsyncFromCache(int id)
        {
            Book? b = await memory.GetOrCreateAsync("Book" + id, async (e) =>
            {
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.Next(10,15));
               
                return await MyDbContext.GetBookByIDAsync(id);
            });
            if (b == null)
                return NotFound("参数不正确");
            else
            {
                return b;
            }
        }
        [HttpGet]
        public int ADD(int num)
        {
            if (num > 10)
                return 88;
            else if (num > 20)
                return 99;
            else
                throw new Exception("参数不正确");
        }
        [HttpGet]
        public IActionResult ADD2(int num)
        {
            if (num > 10)
                return Ok(88);
            else if (num > 20)
                return Ok(99);
            else
                return NotFound("参数不正确");
        }
        [HttpGet]
        public ActionResult<int> ADD3(int num)
        {
            if (num > 10)
                return 88;
            else if (num > 20)
                return 99;
            else
                return NotFound("参数不正确");
        }
        [HttpGet("students/{ClassName}/class/{ClassNum}")]
        public Student GetStudent(string ClassName, int ClassNum)
        {
            return new Student(ClassNum, "LIsi", ClassName);
        }
        [HttpGet("students/{ClassName}/class/{ClassNum}")]
        public Student GetStudent2(string ClassName, [FromRoute(Name = "ClassNum")] int ClassNo)
        {
            return new Student(ClassNo, "LIsi", ClassName);
        }
        [HttpGet("students/{ClassName}/class/{ClassNum}")]
        public Student GetStudent3(string ClassName, [FromRoute(Name = "ClassNum")] int ClassNo, [FromQuery(Name = "ID")] string ID)
        {
            return new Student(ClassNo, ID, ClassName);
        }
        [HttpPut("{ID}")]
        public string Update([FromRoute(Name = "ID")] int id, Student p1)
        {
            return "OK" + p1.Name + id;
        }
        [HttpGet]
        public string Update2(int p1)
        {
            return "OK" + p1;
        }
        [HttpGet]
        public string Update3([FromHeader(Name = "User-Agent")] string p1)
        {
            return "OK" + p1;
        }
    }
}
