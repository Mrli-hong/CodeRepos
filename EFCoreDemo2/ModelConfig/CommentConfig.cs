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
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("T_Comments");
            builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments)
                .IsRequired().HasForeignKey(c => c.ArticleId);
            //builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments).IsRequired();
            builder.Property(c => c.Message).IsRequired().IsUnicode();
        }
    }
}
