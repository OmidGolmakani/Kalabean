using Kalabean.Domain;
using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class OrderHeaderEntitySchemaDefinition :
        IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.ToTable("OrderHeader")
                .HasKey(c => c.Id);

            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.StoreId).IsRequired();
            builder.Property(p => p.FromUserId).IsRequired();
            builder.Property(p => p.OrderPrice).IsRequired();
            builder.Property(p => p.OrderPrice).IsRequired();
            builder.Property(p => p.PaymentLink).HasMaxLength(200);
            builder.Property(p => p.PaymentDate);
            builder.Property(p => p.OrderStatus).IsRequired().HasDefaultValue((byte)OrderStatus.AwaitingApproval);
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Published);
            builder.Property(p => p.OrderNum);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);
            builder.HasOne(p => p.Store).WithMany(p => p.Orders).OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.StoreId);
            builder.HasOne(p => p.FromOrderUser).WithMany(p => p.FromOrderHeaders).OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.FromUserId);
            builder.HasOne(p => p.ToOrderUser).WithMany(p => p.ToOrderHeaders).OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.ToUserId);
        }
    }
}
