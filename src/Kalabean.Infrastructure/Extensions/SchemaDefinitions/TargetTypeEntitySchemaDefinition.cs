using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class TargetTypeEntitySchemaDefinition :
        IEntityTypeConfiguration<TargetType>
    {
        public void Configure(EntityTypeBuilder<TargetType> builder)
        {
            builder.ToTable("TargetType")
                .HasKey(c => c.Id);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);
            builder.Property(c => c.Name).HasMaxLength(120);

        }
    }
}
