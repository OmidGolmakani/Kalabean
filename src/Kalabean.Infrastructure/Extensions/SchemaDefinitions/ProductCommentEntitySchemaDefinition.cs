using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class ProductCommentEntitySchemaDefinition :
        IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.ToTable("ProductComment")
                .HasKey(c => c.Id);
            builder.Property(a => a.IsDeleted)
                .HasDefaultValue(false);
            builder.Property(a => a.LastModified);
            builder.Property(a => a.LastModifiedBy).HasMaxLength(120);
            builder.Property(a => a.CreatedDate);
            builder.Property(a => a.CreatedBy).HasMaxLength(120);

            builder.Property(p => p.Name).HasMaxLength(60);
            builder.Property(p => p.Family).HasMaxLength(60);
            builder.Property(p => p.Email).HasMaxLength(100);
            builder.Property(p => p.PhoneNumber).HasMaxLength(12);
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.UserId);
            builder.Property(p => p.AdminId);
            builder.HasOne(p => p.Product).WithMany(p => p.ProductComments)
                .HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.User).WithMany(p => p.ProductCommentUsers).HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.AdminUser).WithMany(p => p.ProductCommentAdmins).HasForeignKey(p => p.AdminId);
        }
    }
}
