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
            builder.Property(p => p.ParentId);
            builder.Property(p => p.Name).HasMaxLength(60);
            builder.Property(p => p.IsDeleted);
            builder.Property(p => p.Text).HasMaxLength(500);
            builder.Property(p => p.Name).HasMaxLength(120).IsRequired();
            builder.Property(p => p.Title).HasMaxLength(120).IsRequired();
            builder.Property(p => p.HasImage);
            builder.Property(p => p.AdPositionId).IsRequired();
            builder.Property(p => p.UrlLink).HasMaxLength(200).IsRequired();
            builder.HasOne(p => p.Parent).WithMany(p => p.Child)
                .OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.ParentId);

        }
    }
}
