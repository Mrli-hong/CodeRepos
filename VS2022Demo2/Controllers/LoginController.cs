 using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace VS2022Demo2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult<LoginResoponse> Login(LoginRequest req)
        {
            if (req.Password == "123" && req.UserName == "Li")
            {
                var items = Process.GetProcesses().Select(x => new ProcessInfo(x.Id,x.ProcessName,x.WorkingSet64)).ToArray();
                return new LoginResoponse(true, items);
            }
            else
            {
                return new LoginResoponse(false, null);
            }
        }
    }

    public record LoginRequest(string UserName,string Password);
    public record ProcessInfo(int Id,string Name,long WorkingSet);
    public record LoginResoponse(bool Ok, ProcessInfo[]? ProcessInfos);
}
