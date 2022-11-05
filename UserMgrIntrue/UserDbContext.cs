using Microsoft.EntityFrameworkCore;
using UserMgrDomain.Entities;

namespace UserMgrInfrastracture
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<UserLoginHistory> LoginHistories { get; private set; }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
