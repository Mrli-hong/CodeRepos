using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreDemo2.Model;
namespace EFCoreDemo2.ModelConfig
{
    class PartmentConfig : IEntityTypeConfiguration<Partment>
    {
        public void Configure(EntityTypeBuilder<Partment> builder)
        {
            builder.ToTable("T_Partment");
            builder.Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(10);
            //不需要在后面添加isRequired因为根节点没有父节点
            builder.HasOne<Partment>(e => e.OriPartment).WithMany(e => e.ChildrenPartment);
        }
    }
}
