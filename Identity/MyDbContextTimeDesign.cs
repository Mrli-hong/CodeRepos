using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity
{
    public class MyDbContextTimeDesign : IDesignTimeDbContextFactory<MyIdentityDbContext>
    {
        public MyIdentityDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyIdentityDbContext> builder = new DbContextOptionsBuilder<MyIdentityDbContext>();
            builder.UseSqlServer("Server=.;Database=demo1;Trusted_Connection=True;;TrustServerCertificate=true");
            return new MyIdentityDbContext(builder.Options);
        }
    }
}
