using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class RolePermissionEntitySchemaDefinition :
        IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermission")
                .HasKey(c => c.Id);
            builder.Property(p => p.RoleId).IsRequired();
            builder.Property(p => p.Token).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Url).IsRequired().HasMaxLength(300);
        }
    }
}
