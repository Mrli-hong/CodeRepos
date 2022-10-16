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
    public class ArticleConfig : IEntityTypeConfiguration<Article>
    {
		public void Configure(EntityTypeBuilder<Article> builder)
		{
			builder.ToTable("T_Articles");
			builder.Property(a => a.Content).IsRequired().IsUnicode();
			builder.Property(a => a.Title).IsRequired().IsUnicode().HasMaxLength(255);
		}
	}
}
