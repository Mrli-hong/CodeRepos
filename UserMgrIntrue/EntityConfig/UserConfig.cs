using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMgrDomain.Entities;

namespace UserMgrInfrastracture.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("M_Users");
            builder.OwnsOne(x => x.PhoneNumber, nb =>
            {
                nb.Property(b => b.PhoneNumbers).HasMaxLength(20).IsUnicode(false);
            });
            builder.HasOne(b => b.UserAccessFail).WithOne(f => f.User).HasForeignKey<UserAccessFail>(f => f.Id);
            builder.Property("passwordHash").HasMaxLength(100).IsUnicode(false);
        }
    }
}