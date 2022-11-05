using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserMgrAPI;
using UserMgrDomain;
using UserMgrInfrastracture;
using Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(opt =>
{
    opt.UseSqlServer("Server=.;Database=demo1;Trusted_Connection=True;TrustServerCertificate=true;");
});
builder.Services.Configure<MvcOptions>(opt => {
    opt.Filters.Add<UnitOfWorkFilter>();
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "UsersDemo_";
});

builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<ISmsCodeSender, MockSmsCodeSender>();
builder.Services.AddScoped<IUserDomainReposity, UserDomainRepository>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

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
