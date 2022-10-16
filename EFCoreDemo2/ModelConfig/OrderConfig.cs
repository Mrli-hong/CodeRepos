using EFCoreDemo2.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo2.ModelConfig
{
    class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne<User>(e => e.Receiver).WithMany();
            builder.HasOne<User>(e => e.Sender).WithMany().IsRequired();
        }
    }
}
