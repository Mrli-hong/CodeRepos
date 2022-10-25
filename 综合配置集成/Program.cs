using Microsoft.Data.SqlClient;
using StackExchange.Redis;
using System.Reflection.PortableExecutable;
using �ۺ����ü���;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ʹ��Zack����ȡ���ݿ����������ļ���
var webBuilder = builder.Host;
webBuilder.ConfigureAppConfiguration((hostCtx, configBuilder) => {
    string connStr = builder.Configuration.GetSection("ConStr").Value;
    configBuilder.AddDbConfiguration(() => new SqlConnection(connStr), reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(2));
});
//ʹ��redis��������һ��redis���ӵĿͻ���,����һ����Progr   am.cs�ж�ȡ���õķ���
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    string constr = builder.Configuration.GetSection("Redis").Value;
    return ConnectionMultiplexer.Connect(constr);
});

//��ȡSmtpSetting���ö���
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
