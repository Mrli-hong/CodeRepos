using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace SpeedLimiteAction
{
    public class SpeedLimitFilter : IAsyncActionFilter
    {
        private readonly IMemoryCache ch;

        public SpeedLimitFilter(IMemoryCache ch)
        {
            this.ch = ch;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string cacheKey = $"lastvisittick_{ip}";
            long? lastVisit = ch.Get<long?>(cacheKey);
            if (lastVisit == null || Environment.TickCount64-lastVisit>1000)
            {
                ch.Set<long?>(cacheKey, Environment.TickCount64,TimeSpan.FromSeconds(10));
                await next();
            }
            else
            {
                ObjectResult result = new ObjectResult("访问太频繁！")
                {
                    StatusCode = 429
                };
                context.Result = result;
            }
        }
    }
}
