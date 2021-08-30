using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class TicketEntitySchemaDefinition :
        IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket")
                .HasKey(p => p.Id);
            builder.Property(p => p.Title).HasMaxLength(200);
            builder.Property(p => p.SenderUserId).IsRequired();
            builder.Property(p => p.RecipientUserId);
            builder.Property(p => p.Status).IsRequired().HasDefaultValue((byte)TicketStatus.Active);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);
            builder.HasOne(p => p.SenderUser).WithMany(p => p.SenderTickets).
                OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.SenderUserId);
            builder.HasOne(p => p.RecipientUser).WithMany(p => p.RecipientTickets).
                OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.RecipientUserId);
        }
    }
}
