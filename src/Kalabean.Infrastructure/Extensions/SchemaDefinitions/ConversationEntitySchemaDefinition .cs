using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class ConversationEntitySchemaDefinition :
        IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.ToTable("Conversation")
                .HasKey(p => p.Id);
            builder.Property(p => p.RequirementId);
            builder.Property(p => p.Title).HasMaxLength(200);
            builder.Property(p => p.SenderUserId).IsRequired();
            builder.Property(p => p.RecipientUserId);
            builder.Property(p => p.Status).IsRequired().HasDefaultValue((byte)ConversationStatus.Active);
            builder.Property(c => c.LastModified);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(120);
            builder.Property(c => c.CreatedDate);
            builder.Property(c => c.CreatedBy).HasMaxLength(120);
            builder.HasOne(p => p.SenderUser).WithMany(p => p.SenderConversations).
                OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.SenderUserId);
            builder.HasOne(p => p.RecipientUser).WithMany(p => p.RecipientConversations).
                OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.RecipientUserId);
            builder.HasOne(p => p.Requirement).WithMany(p => p.Conversations).
                OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.RequirementId);
        }
    }
}
