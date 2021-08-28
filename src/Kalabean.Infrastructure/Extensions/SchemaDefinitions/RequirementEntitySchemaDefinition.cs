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
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Price).HasPrecision(18, 2).IsRequired();
            builder.Property(p => p.TypePricing).IsRequired().HasDefaultValue(1);
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.DateChangeStatus).IsRequired();
            builder.Property(p => p.Exprie).IsRequired();
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);
            builder.HasOne(p => p.Category).WithMany(p => p.Requirements)
                .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.RequirementUser).WithMany(p => p.RequirementUsers)
                .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.AdminUser).WithMany(p => p.RequirementAdmins)
                .HasForeignKey(p => p.AdminId).OnDelete(DeleteBehavior.NoAction);



        }
    }
}
