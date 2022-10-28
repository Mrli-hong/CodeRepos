using Middle;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/test", async (pipeBuilder) =>
{
    pipeBuilder.UseMiddleware<PassWordMiddle>();
    pipeBuilder.Use(async (context, next) =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("1 start<br/>");
        await next.Invoke();
        await context.Response.WriteAsync("1  End<br/>");
    });
    pipeBuilder.UseMiddleware<MyMiddle>();
    pipeBuilder.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("2  Start<br/>");
        await next.Invoke();
        await context.Response.WriteAsync("2  End<br/>");
    });
    pipeBuilder.Run(async ctx =>
    {
        await ctx.Response.WriteAsync("hello middleware <br/>");
        dynamic? obj = ctx.Items["body"];
        if (obj is not null)
        {
            await ctx.Response.WriteAsync($"{obj}");
        }
    });
});
app.Run();

