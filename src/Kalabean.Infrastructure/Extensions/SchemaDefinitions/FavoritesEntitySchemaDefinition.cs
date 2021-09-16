using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class FavoritesEntitySchemaDefinition :
        IEntityTypeConfiguration<Favorites>
    {
        public void Configure(EntityTypeBuilder<Favorites> builder)
        {
            builder.ToTable("Favorites")
                .HasKey(p => p.Id);

            builder.Property(p => p.RelatedId).IsRequired();
            builder.Property(p => p.TypeId).IsRequired();
            builder.Property(p => p.LastModified);
            builder.Property(p => p.LastModifiedBy).HasMaxLength(120);
            builder.Property(p => p.CreatedDate);
            builder.Property(p => p.CreatedBy).HasMaxLength(120);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}
