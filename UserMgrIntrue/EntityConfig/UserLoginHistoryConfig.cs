using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgrDomain.Entities;

namespace UserMgrInfrastracture.EntityConfig
{
    internal class UserLoginHistoryConfig : IEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
            builder.ToTable("M_UserLoginHistory");
            builder.OwnsOne(x => x.PhoneNumber, y =>
            {
                y.Property(b => b.PhoneNumbers).HasMaxLength(100).IsUnicode(false);
            });
        }
    }
}
