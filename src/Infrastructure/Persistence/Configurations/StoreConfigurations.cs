using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElarabyCA.Infrastructure.Persistence.Configurations
{
    public class StoreConfigurations : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedBy).HasMaxLength(450);

            builder.Property(e => e.LastModified).HasColumnType("datetime");

            builder.Property(e => e.LastModifiedBy).HasMaxLength(450);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(d => d.AdministratorNavigation)
                .WithMany(p => p.Stores)
                .HasForeignKey(d => d.Administrator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Store_Employee");
        }
    }
}
