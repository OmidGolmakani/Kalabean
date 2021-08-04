using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class RuleEntitySchemaDefinition :
        IEntityTypeConfiguration<AccessRule>
    {
        public void Configure(EntityTypeBuilder<AccessRule> builder)
        {
            builder.ToTable("AccessRules")
                .HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired();
        }
    }
}
