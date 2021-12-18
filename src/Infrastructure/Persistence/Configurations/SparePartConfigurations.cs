using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElarabyCA.Infrastructure.Persistence.Configurations
{
    public class SparePartConfigurations : IEntityTypeConfiguration<SparePart>
    {
        public void Configure(EntityTypeBuilder<SparePart> builder)
        {
            builder.Property(e => e.Barcode).HasMaxLength(200);

            builder.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedBy).HasMaxLength(450);

            builder.Property(e => e.LastModified).HasColumnType("datetime");

            builder.Property(e => e.LastModifiedBy).HasMaxLength(450);

            builder.HasOne(d => d.CategoryNavigation)
                .WithMany(p => p.SparePartCategories)
                .HasForeignKey(d => d.Category)
                .HasConstraintName("FK_SparePart_ValueObject");
        }
    }
}
