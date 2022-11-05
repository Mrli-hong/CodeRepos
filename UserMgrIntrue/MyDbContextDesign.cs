using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgrInfrastracture
{
    public class MyDbContext : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            dbContextOptionsBuilder.UseSqlServer("Server=.;Database=demo1;Trusted_Connection=True;TrustServerCertificate=true;");

            return new UserDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
