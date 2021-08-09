using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class RoleEntitySchemaDefinition :
        IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("AspNetRoles")
                .HasKey(c => c.Id);
            builder.Property(p => p.ConcurrencyStamp);
            builder.Property(p => p.Name);
            builder.Property(p => p.NormalizedName);
        }
    }
}
