using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElarabyCA.Infrastructure.Persistence.Configurations
{
    public class InventoryTransactionConfigurations : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.HasKey(e => e.TransactionId);

            builder.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedBy).HasMaxLength(450);

            builder.Property(e => e.LastModified).HasMaxLength(450);

            builder.Property(e => e.LastModifiedBy).HasMaxLength(450);

            builder.HasOne(d => d.Inventory)
                .WithMany(p => p.InventoryTransactions)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryTransaction_Inventory");

            builder.HasOne(d => d.TypeNavigation)
                .WithMany(p => p.InventoryTransactionTypes)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK_InventoryTransaction_ValueObject");
        }
    }
}
