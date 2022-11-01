using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace JWTandVersion
{
    public class JwtFilter : IAsyncActionFilter
    {
        private readonly UserManager<MyUser> userManager;

        public JwtFilter(UserManager<MyUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ControllerActionDescriptor? condes = context.ActionDescriptor as ControllerActionDescriptor;
            //如果方法上标注了NoJWTAttribute
            if (condes == null||condes.MethodInfo.GetCustomAttributes(typeof(NoJWTAttribute), true).Any())
            {
                await next();
                return;
            }
            var jwtversion = context.HttpContext.User.FindFirst("JwtVersion");
            if (jwtversion == null)
            {
                context.Result = new ObjectResult("客户端没有Jwtversion") { StatusCode = 401 };
                return;
            }
            else
            {
                long jwtVersionFromClient = Convert.ToInt64(jwtversion.Value);
                var claimID = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                var user = await userManager.FindByIdAsync(claimID?.Value);
                if (user != null&&user.JwtVersion<=jwtVersionFromClient)
                {
                    await next();
                    return;
                }
                else
                {
                    context.Result = new ObjectResult("客户端Jwtversion超时") { StatusCode = 401 };
                    return;
                }
            }
        }
    }
}
