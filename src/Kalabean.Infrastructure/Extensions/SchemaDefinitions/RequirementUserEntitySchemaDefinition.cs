using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class ArticleUserSeenEntitySchemaDefinition :
        IEntityTypeConfiguration<RequirementUserSeen>
    {
        public void Configure(EntityTypeBuilder<RequirementUserSeen> builder)
        {
            builder.ToTable("RequirementUserSeen").HasKey(r => r.Id);
            builder.Property(r => r.RequirementId).IsRequired();
            builder.Property(r => r.UserId).IsRequired();
            builder.HasOne(r => r.Requirement).WithOne(a => a.RequirementUserSeen).
                OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(r => r.User).WithOne(r => r.RequiremenUserSeen).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
