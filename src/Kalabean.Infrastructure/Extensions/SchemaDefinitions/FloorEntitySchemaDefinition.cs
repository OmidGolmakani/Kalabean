using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class FloorEntitySchemaDefinition :
        IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            builder.ToTable("Floors")
                .HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired();
            builder.HasOne(c => c.ShoppingCenter)
                .WithMany(s => s.Floors)
                .HasForeignKey(c => c.ShoppingCenterId);
            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);
        }
    }
}
