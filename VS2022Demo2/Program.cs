using VS2022Demo2;
using Zack.Commons;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//配置CORS允许前端页面直接访问后端接口
builder.Services.AddCors(h =>
{
    h.AddDefaultPolicy(b =>
    {
        b.WithOrigins(new string[] { "http://localhost:5173", "http://127.0.0.1:5173" }).AllowCredentials().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddScoped<Calculater>();
builder.Services.AddScoped<TestService>();

builder.Services.AddMemoryCache();

var asms = ReflectionHelper.GetAllReferencedAssemblies();
builder.Services.RunModuleInitializers(asms);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
