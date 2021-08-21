using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class PossibilitiesShopCenterEntitySchemaDefinition :
        IEntityTypeConfiguration<PossibilitiesShopCenter>
    {
        public void Configure(EntityTypeBuilder<PossibilitiesShopCenter> builder)
        {
            builder.ToTable("PossibilitiesShopCenter")
                .HasKey(c => c.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(120);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);

        }
    }
}
