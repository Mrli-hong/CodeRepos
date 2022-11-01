using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SignalRHubDemo1;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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

//����JWT
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSetting"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x =>
{
    var jwtOpt = builder.Configuration.GetSection("JwtSetting").Get<JwtSetting>();
    byte[] keyBytes = Encoding.UTF8.GetBytes(jwtOpt.Key);
    var secKey = new SymmetricSecurityKey(keyBytes);
    x.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = secKey
    };
    //WebSocket�޷��Զ��屨��ͷ����Authorization���޷�Я��JWTֻ��ͨ��url��QueryString���ݣ���������Ҫ�ڷ����OnMessageReceived�н���ȡ����
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            Console.WriteLine(accessToken);
            var path = context.HttpContext.Request.Path;
            Console.WriteLine(path);
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/Wechat/ChatRoomHub")))
            {
                Console.WriteLine(111);
                Console.WriteLine(accessToken);
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
    //x.Events = new JwtBearerEvents()
    //{
    //    OnMessageReceived = context =>
    //    {
    //        var accessToken = context.Request.Query["access_token"];
    //        var path = context.Request.Path;
    //        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/Wechat/ChatRoomHub"))
    //        {
    //            context.Token = accessToken;
    //        }
    //        return Task.CompletedTask;
    //    }
    //};

});

//�������ļ��ж�ȡSQLServer�����ַ���֮������EF����SqlServer
string connectionStr = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<MyDbContext>(opt =>
{
    opt.UseSqlServer(connectionStr);
});
builder.Services.AddDataProtection();
builder.Services.AddIdentityCore<MyUser>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireLowercase = false;
    //opt.Password.RequireUppercase = false;
    //�����������ɵ�����
    opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultPhoneProvider;
    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});
var iBuilder = new IdentityBuilder(typeof(MyUser), typeof(MyRole), builder.Services);
iBuilder.AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<MyUser>>()
    .AddRoleManager<RoleManager<MyRole>>();

//����SingalR������Ҫ���CORS
builder.Services.AddSignalR();
string[] url = { "https://localhost:5173", "http://localhost:5173", "http://127.0.0.1:5173", "https://127.0.0.1:5173" };
builder.Services.AddCors(
    options =>
    options.AddDefaultPolicy(builder => builder.WithOrigins(url)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//ʹ���м������CORS,����Redirectionǰ    
app.UseCors();

app.UseHttpsRedirection();

//��JWT���н���
app.UseAuthentication();

app.UseAuthorization();
//�����м����hub���д�����Ҫд��Controllersǰ��
app.MapHub<MyHub>("/Wechat/ChatRoomHub");

app.MapControllers();

app.Run();
