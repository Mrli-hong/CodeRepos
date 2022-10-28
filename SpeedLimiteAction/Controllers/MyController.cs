using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpeedLimiteAction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetString()
        {
            return "Heelo";
        }
    }
}
