using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo1
{
    public class MyDbContext:DbContext
    {
        //可以将转化的SQL语句转化为日志的形式输出
        public static ILoggerFactory logger = LoggerFactory.Create(b => b.AddConsole());
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //使用SqlServer连接
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EFCoreDemo1;Integrated Security=true;");

            //1.日志方式输出SQL语句
            //optionsBuilder.UseLoggerFactory(logger);

            //2.简单方式输出SQL语句
            //optionsBuilder.LogTo(msg =>
            //{
            //    Console.WriteLine(msg);
            //});
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //从当前程序集中加载配置项
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
