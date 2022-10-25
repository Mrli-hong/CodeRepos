var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "Li_";
});

var app = builder.Build();
//查看当前环境变量的值
Console.WriteLine(app.Environment.EnvironmentName);
Console.WriteLine(app.Environment.IsDevelopment());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
Console.WriteLine(app.Configuration.GetSection("Con").Value);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
