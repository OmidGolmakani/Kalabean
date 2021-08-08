using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class RequirementEntitySchemaDefinition :
        IEntityTypeConfiguration<Requirement>
    {
        public void Configure(EntityTypeBuilder<Requirement> builder)
        {
            builder.ToTable("Requirement")
                .HasKey(c => c.Id);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.HasImage).HasDefaultValue(false);
            builder.Property(p => p.AdminDate);
            builder.Property(p => p.AdminId);
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Price).HasPrecision(18, 2).IsRequired();
            builder.Property(p => p.TypePricing).IsRequired().HasDefaultValue(1);
            builder.Property(p => p.UserId).IsRequired();
            builder.HasOne(p => p.Category).WithMany(p => p.Requirements)
                .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Product).WithMany(p => p.Requirements)
                .HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.NoAction);



        }
    }
}
