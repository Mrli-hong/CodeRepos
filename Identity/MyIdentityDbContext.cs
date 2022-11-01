using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class MyIdentityDbContext:IdentityDbContext<MyUser,MyRole,long>
    {
        public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options)
        { }
    }
}
