using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElarabyCA.Infrastructure.Persistence.Configurations
{
    public class OrderTicketConfigurations : IEntityTypeConfiguration<OrderTicket>
    {
        public void Configure(EntityTypeBuilder<OrderTicket> builder)
        {
            builder.HasKey(e => e.Oid);

            builder.HasIndex(e => e.Category)
                .HasName("iCategory_OrderTicket");

            builder.HasIndex(e => e.Employee)
                .HasName("iEmployee_OrderTicket");

            builder.HasIndex(e => e.Gcrecord)
                .HasName("iGCRecord_OrderTicket");

            builder.HasIndex(e => e.Order)
                .HasName("iOrder_OrderTicket");

            builder.Property(e => e.Oid).HasColumnName("OID");

            builder.Property(e => e.Date).HasColumnType("datetime");

            builder.Property(e => e.Gcrecord).HasColumnName("GCRecord");

            builder.Property(e => e.UserName).HasMaxLength(100);

            builder.HasOne(d => d.CategoryNavigation)
                .WithMany(p => p.OrderTicket)
                .HasForeignKey(d => d.Category)
                .HasConstraintName("FK_OrderTicket_Category");

            builder.HasOne(d => d.EmployeeNavigation)
                .WithMany(p => p.OrderTicket)
                .HasForeignKey(d => d.Employee)
                .HasConstraintName("FK_OrderTicket_Employee");

            builder.HasOne(d => d.Order1)
                .WithMany(p => p.OrderTicket)
                .HasForeignKey(d => d.Order)
                .HasConstraintName("FK_OrderTicket_Order");
        }
    }
}
