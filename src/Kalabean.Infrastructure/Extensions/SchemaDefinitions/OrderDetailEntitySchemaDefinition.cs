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
    public class OrderDetailEntitySchemaDefinition :
        IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail")
            .HasKey(c => c.Id);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.OrderId).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Num).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();
            builder.HasOne(p => p.OrderHeader).WithMany(p => p.OrderDetails).OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(p => p.OrderId);
            builder.HasOne(p => p.Product).WithMany(p => p.OrderDetails).OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
