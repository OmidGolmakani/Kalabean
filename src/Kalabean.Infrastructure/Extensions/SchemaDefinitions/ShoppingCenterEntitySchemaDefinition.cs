using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class ShoppingCenterEntitySchemaDefinition :
        IEntityTypeConfiguration<ShoppingCenter>
    {
        public void Configure(EntityTypeBuilder<ShoppingCenter> builder)
        {
            builder.ToTable("ShoppingCenters")
                .HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(s => s.City)
                .WithMany(c => c.ShoppingCenters)
                .HasForeignKey(s => s.CityId);

            builder.HasOne(s => s.Type)
                .WithMany(c => c.ShoppingCenters)
                .HasForeignKey(s => s.TypeId);
        }
    }
}
