 using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace VS2022Demo2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly Calculater calculater;
        private readonly Class1 cs;
        private readonly IMemoryCache memoryCache;
        public LoginController(Calculater calculater, Class1 cs, IMemoryCache memoryCache)
        {
            this.calculater = calculater;
            this.cs = cs;
            this.memoryCache = memoryCache;
        }

        [HttpPost]
        public ActionResult<LoginResoponse> Login(LoginRequest req)
        {
            if (req.Password == "123" && req.UserName == "Li")
            {
                var items = Process.GetProcesses().Select(x => new ProcessInfo(x.Id, x.ProcessName, x.WorkingSet64)).ToArray();
                return new LoginResoponse(true, items);
            }
            else
            {
                return new LoginResoponse(false, null);
            }
        }
        //[HttpGet]
        //[ResponseCache(Duration = 60)]
        //public int add(int x, int y)
        //{
        //    return calculater.Add(x, y);
        //}
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public DateTime add()
        {
            return DateTime.Now;
        }
        [HttpPut]
        public int TestService([FromServices]TestService ts, int y)
        {
            return ts.Count+y;
        }
        [HttpDelete]
        public string TestService2()
        {
            return cs.ConsoleService();
        }
        [ResponseCache(Duration =60)]
        [HttpPatch]
        public DateTime TestService3()
        {
            return DateTime.Now;
        }
    }

    public record LoginRequest(string UserName, string Password);
    public record ProcessInfo(int Id, string Name, long WorkingSet);
    public record LoginResoponse(bool Ok, ProcessInfo[]? ProcessInfos);
}
