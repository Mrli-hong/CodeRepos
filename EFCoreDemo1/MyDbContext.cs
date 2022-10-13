using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo1
{
    public class MyDbContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //使用SqlServer连接
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EFCoreDemo1;Integrated Security=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //从当前程序集中加载配置项
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
