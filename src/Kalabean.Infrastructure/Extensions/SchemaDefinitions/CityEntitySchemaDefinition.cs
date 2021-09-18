using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class CityEntitySchemaDefinition :
        IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities")
                .HasKey(c => c.Id);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);

            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c => c.Order);
            builder.Property(c => c.ParentId);
            builder.Property(c => c.HasImage).HasDefaultValue(false);
            builder.Property(c => c.State).HasDefaultValue((byte)CityState.State);
            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);
            builder.HasOne(c => c.Parent).WithMany(c => c.Child).HasForeignKey(c => c.ParentId);

        }
    }
}
