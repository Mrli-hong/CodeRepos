using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IOptionsSnapshot<JwtSetting> setting;

        public DemoController(IOptionsSnapshot<JwtSetting> setting)
        {
            this.setting = setting;
        }

        [HttpPost]
        public ActionResult<string> Login(string userName, string passWord)
        {
            //登陆成功生成JWT签名
            if (userName == "123")
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("UserName", "LHY"));
                claims.Add(new Claim("Password", "123.com"));
                claims.Add(new Claim(ClaimTypes.GroupSid, "421"));
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                claims.Add(new Claim(ClaimTypes.Role, "user"));

                string key = setting.Value.Key;
                DateTime expire = DateTime.Now.AddSeconds(setting.Value.ExpireSeconds);
                byte[] secBytes = Encoding.UTF8.GetBytes(key);
                var serKey = new SymmetricSecurityKey(secBytes);
                var credentials = new SigningCredentials(serKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: credentials);
                string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                return jwt;
            }
            else
            {
                return BadRequest();
            }  
        }
    }
}
