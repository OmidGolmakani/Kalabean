using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class AdvertiseEntitySchemaDefinition :
        IEntityTypeConfiguration<Advertise>
    {
        public void Configure(EntityTypeBuilder<Advertise> builder)
        {
            builder.ToTable("Advertise")
                .HasKey(c => c.Id);
            builder.Property(a => a.LastModified);
            builder.Property(a => a.LastModifiedBy).HasMaxLength(120);
            builder.Property(a => a.CreatedDate);
            builder.Property(a => a.CreatedBy).HasMaxLength(120);

            builder.Property(p => p.Text).HasMaxLength(500);
            builder.Property(p => p.Title).HasMaxLength(120).IsRequired();
            builder.Property(p => p.HasImage);
            builder.Property(p => p.AdPositionId).IsRequired();
            builder.Property(p => p.UrlLink).HasMaxLength(200);

        }
    }
}
