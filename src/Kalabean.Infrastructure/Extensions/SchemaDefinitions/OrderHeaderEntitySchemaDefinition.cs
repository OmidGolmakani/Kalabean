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
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.OrderPrice).IsRequired();
            builder.Property(p => p.OrderPrice).IsRequired();
            builder.Property(p => p.PaymenyLink).HasMaxLength(200);
            builder.Property(p => p.OrderStatus).IsRequired().HasDefaultValue((byte)OrderStatus.AwaitingApproval);
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.HasOne(p => p.Store).WithMany(p => p.Orders).OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.StoreId);
        }
    }
}
