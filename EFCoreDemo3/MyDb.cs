using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo3
{
    class MyDb:DbContext
    {
        public DbSet<Rent> rents { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(Console.WriteLine);
            var connectionString = "server=10.135.70.143;user=MrLi;password=123.com;database=dbtest1;Port=13306";
            var serverVersion = new MySqlServerVersion(new Version(5, 7, 34));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
