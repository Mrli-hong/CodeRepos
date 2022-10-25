using Microsoft.Data.SqlClient;
using StackExchange.Redis;
using System.Reflection.PortableExecutable;
using 综合配置集成;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//使用Zack包读取数据库中心配置文件。
var webBuilder = builder.Host;
webBuilder.ConfigureAppConfiguration((hostCtx, configBuilder) => {
    string connStr = builder.Configuration.GetSection("ConStr").Value;
    configBuilder.AddDbConfiguration(() => new SqlConnection(connStr), reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(2));
});
//使用redis包，生成一个redis连接的客户端,这是一个在Progr   am.cs中读取配置的方法
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    string constr = builder.Configuration.GetSection("Redis").Value;
    return ConnectionMultiplexer.Connect(constr);
});

//获取SmtpSetting配置对象
builder.Services.Configure<SmtpSetting>(builder.Configuration.GetSection("Smtp"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
