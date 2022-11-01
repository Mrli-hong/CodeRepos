using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalRHubDemo1
{
    public class MyRole : IdentityRole<long>
    {
    }
    public class MyUser : IdentityUser<long>
    {
        public long JwtVersion { get; set; }
    }
    public class MyDbContext : IdentityDbContext<MyUser, MyRole, long>
    {
        public MyDbContext(DbContextOptions options) : base(options)
        { }
    }
    public class JwtSetting
    {
        public string Key { get; set; }
        public int ExpireSeconds { get; set; }
    }
    public static class JwtManager
    {
        public static async Task<string> JwtGenerate(MyUser user,JwtSetting setting,UserManager<MyUser> userManager)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim("JWTVersion", user.JwtVersion.ToString()));
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            string key = setting.Key;
            DateTime expire = DateTime.Now.AddMinutes(setting.ExpireSeconds);
            byte[] secBytes = Encoding.UTF8.GetBytes(key);
            var serKey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(serKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            //Console.WriteLine(jwt);
            return jwt;
        }
    }
}
