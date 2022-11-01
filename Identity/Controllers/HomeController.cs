using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<MyUser> userManager;
        private readonly RoleManager<MyRole> roleManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(UserManager<MyUser> userManager, RoleManager<MyRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 用户获取修改密码得 Token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Modified(string UserName)
        {
            string userName = UserName;
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound("用户错误");
            }
            else
            {
                string token = await userManager.GeneratePasswordResetTokenAsync(user);
                Console.WriteLine(token);
                return Ok(token);
            }
        }
        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ModifiedPassword(string token)
        {
            var user = await userManager.FindByNameAsync("LHY");
            var result = await userManager.ResetPasswordAsync(user, token, "123.Com");
            if (result.Succeeded)
                return Ok("密码修改成功");
            else
                return BadRequest("密码修改失败");

        }
        /// <summary>
        /// 用户登录检测
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Login(LogReq req)
        {
            string passsWord = req.Password;
            string userName = req.UserName;
            //查找到用户
            var user = await userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return BadRequest("用户名不存在");
            }
            else
            {
                //检测用户是否被锁定
                if (await userManager.IsLockedOutAsync(user))
                {
                    return BadRequest($"用户被锁定锁定时间为{user.LockoutEnd}");
                }
                //检测密码是否正确
                var result = await userManager.CheckPasswordAsync(user, passsWord);
                if (!result)
                {
                    await userManager.AccessFailedAsync(user);
                    return BadRequest("密码错误");
                }
                await userManager.ResetAccessFailedCountAsync(user);
                return Ok("登录成功");
            }
        }

        /// <summary>
        /// 用户创建并赋予权限。
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Text()
        {
            if (await roleManager.RoleExistsAsync("admin") == false)
            {
                MyRole role = new MyRole() { Name = "admin" };
                IdentityResult result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return BadRequest($"{result.Errors.ToString()}");
                }
            }
            MyUser myUser = await userManager.FindByNameAsync("LHY");
            if (myUser is null)
            {
                myUser = new MyUser() { UserName = "LHY" };
                var result = await userManager.CreateAsync(myUser, "12345678asAd！");
                if (!result.Succeeded)
                {
                    return BadRequest($"{result.Errors.ToString()}");
                }
                if (await userManager.IsInRoleAsync(myUser, "admin") == false)
                {
                    var result2 = await userManager.AddToRoleAsync(myUser, "admin");
                    if (!result.Succeeded)
                    {
                        return BadRequest($"用户添加到角色失败");
                    }
                }
            }
            return Ok($"创建成功{myUser.ToString()}");
            //if (webHostEnvironment.IsDevelopment())
            //{
            //    return BadRequest($"用户名不存在！");
            //}
            //return BadRequest();
        }
    }
}
