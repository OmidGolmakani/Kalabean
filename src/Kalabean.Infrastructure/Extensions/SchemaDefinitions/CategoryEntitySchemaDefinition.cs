using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class CategoryEntitySchemaDefinition :
        IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories")
                .HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);
            builder.
                HasOne(c => c.Parent).
                WithMany(c => c.Children).
                HasForeignKey(c => c.ParentId);

            builder
                .HasOne(c => c.AccessRule)
                .WithMany(ar => ar.Category)
                .HasForeignKey(c => c.AccessRuleId);
        }
    }
}
