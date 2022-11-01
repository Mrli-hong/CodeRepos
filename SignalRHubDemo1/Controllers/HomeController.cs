using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SignalRHubDemo1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<MyUser> userManager;
        private readonly RoleManager<MyRole> roleManager;
        private readonly IOptions<JwtSetting> jwtSetting;
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="jwtSetting"></param>
        public HomeController(UserManager<MyUser> userManager, RoleManager<MyRole> roleManager, IOptions<JwtSetting> jwtSetting)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtSetting = jwtSetting;
        }
        /// <summary>
        /// 登录功能，配发JWT
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginReq req)
        {
            string username = req.UserName;
            string password = req.Password;
            var User = await userManager.FindByNameAsync(username);
            try
            {
                if (User is not null)
                {
                    if (await userManager.CheckPasswordAsync(User, password))
                    {
                        await userManager.ResetAccessFailedCountAsync(User).CheckAsync();
                        string jwt = await JwtManager.JwtGenerate(User, jwtSetting.Value, userManager);
                        return Ok($"{jwt}");
                    }
                    else
                    {
                        await userManager.AccessFailedAsync(User);
                        return BadRequest("登陆失败");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("账户不存在");
        }
        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> CreateUser(CreateUserReq req, [FromServices] MyDbContext db)
        {
            string username = req.UserNmae;
            string password = req.Password;
            var User = new MyUser() { UserName = username };
            try
            {
                using (var tx = await db.Database.BeginTransactionAsync())
                {
                    await userManager.CreateAsync(User).CheckAsync();
                    await userManager.AddPasswordAsync(User, password).CheckAsync();
                    if (!await roleManager.RoleExistsAsync("admin"))
                    {
                        var Role = new MyRole() { Name = "admin" };
                        await roleManager.CreateAsync(Role).CheckAsync();
                    }
                    var roles = new List<string> { "admin" };
                    await userManager.AddToRolesAsync(User, roles).CheckAsync();
                    tx.Commit();
                    return Ok("角色创建成功");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 重置密码发送Token
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> SendToken(string UserName)
        {
            try
            {
                string token = "null";
                var User = await userManager.FindByNameAsync(UserName);
                if (User != null)
                {
                    token = await userManager.GeneratePasswordResetTokenAsync(User);
                }
                return Ok(token);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 重置或修改密码操作
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> ModifiedPassWord(string userName, string token, string newpassword)
        {
            var User = await userManager.FindByNameAsync(userName);
            try
            {
                if (User is not null)
                {
                    await userManager.ResetPasswordAsync(User, token, newpassword).CheckAsync();
                    return Ok("修改成功");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("账户不存在");
        }
    }
}
