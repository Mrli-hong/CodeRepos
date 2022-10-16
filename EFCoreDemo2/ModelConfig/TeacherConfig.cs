using EFCoreDemo2.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo2.ModelConfig
{
    public class TeacherConfig : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("T_Teachers");
            builder.HasMany<Student>(e => e.Students).WithMany(e => e.Teachers).UsingEntity(e=>e.ToTable("T_Student_Teacher"));
        }
    }
}
