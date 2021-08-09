using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class UserEntitySchemaDefinition :
        IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUsers")
                .HasKey(c => c.Id);
            builder.Property(p => p.Name).HasMaxLength(60);
            builder.Property(p => p.Family).HasMaxLength(60);
            builder.Property(p => p.UserName).HasMaxLength(100);
            builder.Property(p => p.NormalizedUserName).HasMaxLength(100);
            builder.Property(p => p.PasswordHash).HasMaxLength(200);
            builder.Property(p => p.Email).HasMaxLength(70);
            builder.Property(p => p.NormalizedEmail).HasMaxLength(70);
            builder.Property(p => p.PhoneNumber).HasMaxLength(12);
            builder.Property(p => p.PhoneNumberConfirmed).HasDefaultValue(false);
            builder.Property(p => p.EmailConfirmed).HasDefaultValue(false);
            builder.Property(p => p.AccessFailedCount).HasDefaultValue(0);
            builder.Property(p => p.ConcurrencyStamp).HasMaxLength(200).IsRequired();
            builder.Property(p => p.SecurityStamp).HasMaxLength(2000).IsRequired();
            builder.Property(p => p.TwoFactorEnabled);
            builder.Property(p => p.LockoutEnd);
            builder.Property(p => p.LockoutEnabled);
        }
    }
}
