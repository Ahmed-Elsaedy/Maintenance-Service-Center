using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElarabyCA.Infrastructure.Persistence.Configurations
{
    public class InventoryConfigurations : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedBy).HasMaxLength(450);

            builder.Property(e => e.LastModified).HasMaxLength(450);

            builder.Property(e => e.LastModifiedBy).HasMaxLength(450);

            builder.HasOne(d => d.SparePart)
                .WithMany(p => p.Inventories)
                .HasForeignKey(d => d.SparePartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_SparePart");

            builder.HasOne(d => d.Store)
                .WithMany(p => p.Inventories)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_Store");
        }
    }
}
