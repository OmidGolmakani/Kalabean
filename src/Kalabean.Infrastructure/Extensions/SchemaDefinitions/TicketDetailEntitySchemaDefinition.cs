using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class TicketDetailEntitySchemaDefinition :
        IEntityTypeConfiguration<TicketDetail>
    {
        public void Configure(EntityTypeBuilder<TicketDetail> builder)
        {
            builder.ToTable("TicketDetail")
                .HasKey(p => p.Id);
            builder.Property(p => p.Message).IsRequired();
            builder.Property(p => p.TicketId).IsRequired();
            builder.Property(p => p.SenderUserId).IsRequired();
            builder.Property(p => p.DateSeen);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);
            builder.HasOne(p => p.Ticket).WithMany(p => p.TicketDetails).
                OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.TicketId);
            builder.HasOne(p => p.SenderUser).WithMany(p => p.TicketDetails).
                OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.SenderUserId);
        }
    }
}
