using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class ProductEntitySchemaDefinition : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product")
            .HasKey(p => p.Id);
            builder.Property(P => P.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.Manufacturer).HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.Price).HasPrecision(18, 2).IsRequired();
            builder.Property(p => p.Num).HasDefaultValue(0);
            builder.Property(p => p.Discount).HasPrecision(18, 2);
            builder.Property(p => p.Model).HasMaxLength(20);
            builder.Property(p => p.ProductName).HasMaxLength(200);
            builder.Property(p => p.Series).HasMaxLength(20);
            builder.Property(p => p.StoreId).IsRequired();
            builder.Property(p => p.Properties).HasMaxLength(500);
            builder.Property(p => p.LinkProduct).HasMaxLength(200);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);
            builder.Property(c => c.TargetTypeId);

            builder.HasOne(p => p.Category).WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Store).WithMany(p => p.Products)
                .HasForeignKey(p => p.StoreId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.TargetType).WithMany(p => p.Products)
              .HasForeignKey(p => p.TargetTypeId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.User).WithMany(p => p.ProductUsers)
                .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
