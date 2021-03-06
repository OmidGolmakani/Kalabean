using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class StoreEntitySchemaDefinition :
        IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Stores")
                .HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.ShoppingCenterId)
                .IsRequired();
            builder.Property(c => c.CategoryId)
                .IsRequired();

            builder.Property(c => c.DiscountCoupon)
            .HasDefaultValue(0).HasPrecision(18, 2);

            builder.Property(c => c.AuctionPercentage)
                .HasDefaultValue(0);
            builder.Property(c => c.DiscountPercentage)
                .HasDefaultValue(0);

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);

            builder.HasOne(c => c.Category)
                .WithMany(c => c.Stores)
                .HasForeignKey(c => c.CategoryId);

            builder.HasOne(c => c.Floor)
                .WithMany(c => c.Stores)
                .HasForeignKey(c => c.FloorId);

            builder.HasOne(c => c.ShoppingCenter)
                .WithMany(c => c.Stores)
                .HasForeignKey(c => c.ShoppingCenterId);

            builder.HasOne(c => c.StoreUser)
                .WithMany(c => c.Stores)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
