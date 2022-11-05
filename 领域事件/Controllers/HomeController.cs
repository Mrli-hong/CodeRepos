using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace 领域事件.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public ActionResult<string> SayHello()
        {
            mediator.Publish(new PostNotifaction("你好啊" + DateTime.Now));
            return Ok("挺好的");
        }
    }
}
