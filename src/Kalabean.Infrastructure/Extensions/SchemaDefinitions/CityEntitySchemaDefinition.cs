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

            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

        }
    }
}
