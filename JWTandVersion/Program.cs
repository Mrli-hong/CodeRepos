using JWTandVersion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//添加Authorization
builder.Services.AddSwaggerGen(c =>
{
    var scheme = new OpenApiSecurityScheme()
    {
        Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Authorization"
        },
        Scheme = "oauth2",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    };
    c.AddSecurityDefinition("Authorization", scheme);
    var requirement = new OpenApiSecurityRequirement();
    requirement[scheme] = new List<string>();
    c.AddSecurityRequirement(requirement);
});

//增加JwtFilter全局过滤器，验证Jwt是否失效
builder.Services.Configure<MvcOptions>(opt => {
    opt.Filters.Add<JwtFilter>();
});

builder.Services.AddDbContext<MyDbContext>(opt =>
{
    opt.UseSqlServer("Server=.;Database=Job;Trusted_Connection=True;;TrustServerCertificate=true");
});
//增加数据保护服务
builder.Services.AddDataProtection();
//使用AddIdentityCore而不是AddIdentity，AddIdentity为MVC项目服务会生成一些重置密码的页面
builder.Services.AddIdentityCore<MyUser>(opt => {
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
   //重置密码生成的链接
   opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultPhoneProvider;
    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});
var idBuilder = new IdentityBuilder(typeof(MyUser), typeof(MyRole), builder.Services);
idBuilder.AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<MyUser>>()
    .AddRoleManager<RoleManager<MyRole>>();

builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JWT"));

//通过AddAuthentication使得程序可以提取报文头部中Authorization部分并解析
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        var jwtSettings = builder.Configuration.GetSection("JWT").Get<JwtSetting>();
        byte[] keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Key);
        var secKey = new SymmetricSecurityKey(keyBytes);
        opt.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = secKey
        };
    });


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
