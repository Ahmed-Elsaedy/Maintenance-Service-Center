using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElarabyCA.Infrastructure.Persistence.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Oid);

            builder.HasIndex(e => e.ActiveTicket)
                .HasName("iActiveTicket_Order");

            builder.HasIndex(e => e.Gcrecord)
                .HasName("iGCRecord_Order");

            builder.Property(e => e.Oid).HasColumnName("OID");

            builder.Property(e => e.City).HasMaxLength(100);

            builder.Property(e => e.Customer).HasMaxLength(100);

            builder.Property(e => e.DateAssigned).HasColumnType("datetime");

            builder.Property(e => e.DateLastDelayed).HasColumnType("datetime");

            builder.Property(e => e.Gcrecord).HasColumnName("GCRecord");

            builder.Property(e => e.Model).HasMaxLength(100);

            builder.Property(e => e.Orderid)
                .HasColumnName("ORDERID")
                .HasMaxLength(100);

            builder.Property(e => e.OverviewNotes).HasMaxLength(100);

            builder.Property(e => e.PrimaryPhone).HasMaxLength(100);

            builder.Property(e => e.Product).HasMaxLength(100);

            builder.Property(e => e.Region).HasMaxLength(100);

            builder.Property(e => e.ReportsOverview).HasMaxLength(100);

            builder.Property(e => e.Sapnumber).HasColumnName("SAPNumber");

            builder.Property(e => e.SecondaryPhone).HasMaxLength(100);

            builder.Property(e => e.Street).HasMaxLength(100);

            builder.Property(e => e.Tag).HasMaxLength(100);

            builder.Property(e => e.Town).HasMaxLength(100);

            builder.HasOne(d => d.ActiveTicketNavigation)
                .WithMany(p => p.OrderNavigation)
                .HasForeignKey(d => d.ActiveTicket)
                .HasConstraintName("FK_Order_ActiveTicket");

        }
    }
}
