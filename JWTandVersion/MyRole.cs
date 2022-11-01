using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTandVersion
{
    public class MyUser:IdentityUser<long>
    {
        public string? WeixinNumber { get; set; }
        public long JwtVersion { get; set; }
    }
    public class MyRole : IdentityRole<long>
    {
    }
    public class MyDbContext : IdentityDbContext<MyUser, MyRole, long>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        { }
    }
}
