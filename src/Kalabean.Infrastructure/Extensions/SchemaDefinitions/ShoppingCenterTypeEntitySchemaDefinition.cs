using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class ShoppingCenterTypeEntitySchemaDefinition :
        IEntityTypeConfiguration<ShoppingCenterType>
    {
        public void Configure(EntityTypeBuilder<ShoppingCenterType> builder)
        {
            builder.ToTable("ShoppingCenterTypes")
                .HasKey(c => c.Id);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);
            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

        }
    }
}
