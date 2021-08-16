using Kalabean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalabean.Infrastructure.Extensions.SchemaDefinitions
{
    public class ArticleEntitySchemaDefinition :
        IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Article")
                .HasKey(a => a.Id);

            builder.Property(a => a.IsDeleted)
                .HasDefaultValue(false);
            builder.Property(a => a.LastModified);
            builder.Property(a => a.LastModifiedBy).HasMaxLength(120);
            builder.Property(a => a.CreatedDate);
            builder.Property(a => a.CreatedBy).HasMaxLength(120);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
            builder.Property(a => a.KeyWords).HasMaxLength(500);
            builder.Property(a => a.Summary).HasMaxLength(1000);
            builder.Property(a => a.Description).HasMaxLength(1000);
            builder.Property(a => a.HasImage);
            builder.Property(a => a.HasFile);
            builder.Property(a => a.FileExtention).HasMaxLength(5);
            builder.Property(a => a.SuggestedContent);
            builder.Property(a => a.ShowInPortal);
            builder.Property(a => a.AdminId).IsRequired();
            builder.HasOne(a => a.AdminUser).WithMany(a => a.Articles)
                .OnDelete(DeleteBehavior.NoAction).HasForeignKey(a => a.AdminId);

        }
    }
}
