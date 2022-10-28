using Dynamic.Json;

namespace Middle
{
    public class PassWordMiddle
    {
        private readonly RequestDelegate next;

        public PassWordMiddle(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? password = context.Request.Query["password"];
            if (password == "123")
            {
                if (context.Request.HasJsonContentType())
                {
                    var stream = context.Request.BodyReader.AsStream();
                    //System.Text.Json不支持将JSON转化为Dynamic引入Dynamic.Json第三方包
                    dynamic? body = DJson.Parse(stream);
                    context.Items["body"] = body;
                }
                await next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
