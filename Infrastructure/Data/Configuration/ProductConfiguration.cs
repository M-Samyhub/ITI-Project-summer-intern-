using ItiFinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItiFinalProject.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(d => d.Description)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(q => q.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(p => p.ImgePath)
                .IsRequired(false)
                .HasMaxLength(250);

            /*============= Relation =========== */
            //One-to-Many
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            /* ================================ */

            builder.HasIndex(p => p.CategoryId);
            builder.HasIndex(p => p.Title);
        }
    }
}
