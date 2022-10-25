using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Net.NetworkInformation;

namespace 综合配置集成.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly IOptions<SmtpSetting> options;
        private readonly IConnectionMultiplexer connection;

     
        public HelloController(IOptions<SmtpSetting> options, IConnectionMultiplexer connection)
        {
            this.options = options;
            this.connection = connection;
        }
        [HttpGet]
        public string Demo1()
        {
            var ping= connection.GetDatabase(0).Ping();
            return options.Value.ToString() + " " + ping;
        }
    }
}
