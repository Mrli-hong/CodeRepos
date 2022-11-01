using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Demo2Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Test1()
        {
            return Ok("成功1");
        }

        [HttpGet]
        //允许忽略验证
        [AllowAnonymous]
        public ActionResult<string> Test2()
        {
            return Ok("成功2");
        }
    }
}
