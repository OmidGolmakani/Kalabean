using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class ConversationDetailEntitySchemaDefinition :
        IEntityTypeConfiguration<ConversationDetail>
    {
        public void Configure(EntityTypeBuilder<ConversationDetail> builder)
        {
            builder.ToTable("ConversationDetail")
                .HasKey(p => p.Id);
            builder.Property(p => p.Message).IsRequired();
            builder.Property(p => p.ConversationId).IsRequired();
            builder.Property(p => p.SenderUserId).IsRequired();
            builder.Property(p => p.DateSeen);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);
            builder.HasOne(p => p.Conversation).WithMany(p => p.ConversationDetails).
                OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.ConversationId);
            builder.HasOne(p => p.SenderUser).WithMany(p => p.ConversationDetails).
                OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.SenderUserId);
        }
    }
}
