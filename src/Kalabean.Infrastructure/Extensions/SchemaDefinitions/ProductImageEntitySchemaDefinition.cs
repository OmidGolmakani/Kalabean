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
    public class ProductImageEntitySchemaDefinition : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImage")
            .HasKey(p => p.Id);
            builder.Property(pi => pi.IsDeleted).HasDefaultValue(false);
            builder.Property(pi => pi.ProductId).IsRequired();
            builder.Property(pi => pi.Extention).HasMaxLength(5);
            builder.HasOne(pi => pi.Product).WithMany(p => p.ProductImages).HasForeignKey(pi => pi.ProductId);
        }
    }
}
