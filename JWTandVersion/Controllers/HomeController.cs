using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTandVersion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<MyUser> userManager;
        private readonly RoleManager<MyRole> roleManager;
        private readonly IOptionsSnapshot<JwtSetting> settings;

        public HomeController(UserManager<MyUser> userManager, RoleManager<MyRole> roleManager, IOptionsSnapshot<JwtSetting> settings)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.settings = settings;
        }


        /// <summary>
        /// 用户创建并添加到角色中
        /// </summary>
        /// <param name="UserName">LHY</param>
        /// <param name="Password">123.com</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Create(string UserName, string Password)
        {
            if (await roleManager.RoleExistsAsync("admin") == false)
            {
                MyRole myRole = new MyRole() { Name = "admin" };
                try
                {
                    await roleManager.CreateAsync(myRole).CheckAsync();
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
            MyUser myUser = new MyUser() { UserName = UserName };
            try
            {
                await userManager.CreateAsync(myUser, Password).CheckAsync();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            await userManager.AddToRoleAsync(myUser, "admin");
            return Ok("用户创建成功");
        }

        [HttpPost]
        [NoJWT]
        public async Task<ActionResult<string>> Login(string UserName, string Password)
        {
            var User = await userManager.FindByNameAsync(UserName);
            if (User is null)
            {
                return BadRequest("用户名不存在！");
            }
            if (await userManager.CheckPasswordAsync(User, Password))
            {
                //登陆成功清空登录错误次数
                await userManager.ResetAccessFailedCountAsync(User).CheckAsync();

                //登录成功服务端Jwtversion字段自增,并保存到数据库
                User.JwtVersion++;
                await userManager.UpdateAsync(User).CheckAsync();

                //根据用户信息生成payload
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("JwtVersion", User.JwtVersion.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, User.UserName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()));
                var roles =await userManager.GetRolesAsync(User);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                
                //生成JWT并返回
                string key = settings.Value.Key;
                DateTime expire = DateTime.Now.AddSeconds(settings.Value.ExpireSeconds);
                byte[] bytes = Encoding.UTF8.GetBytes(key);
                var secKey = new SymmetricSecurityKey(bytes);
                var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: credentials);
                string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                return Ok(jwt);
            }
            else
            {
                await userManager.AccessFailedAsync(User);
                return BadRequest("密码出错！");
            }

        }
    }
}
