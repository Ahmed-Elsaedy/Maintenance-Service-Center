using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Infrastructure.Persistence.Configurations
{
    public class FincanicalTransactionConfigurations : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
        {
            builder.HasKey(e => e.TransactionId);

            builder.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedBy).HasMaxLength(450);

            builder.Property(e => e.LastModified).HasMaxLength(450);

            builder.Property(e => e.LastModifiedBy).HasMaxLength(450);

            builder.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

            builder.Property(e => e.Amount)
                    .IsRequired();

            builder.Property(e => e.Type)
                    .IsRequired();

            builder.HasOne(d => d.EmployeeNavigation)
                .WithMany(p => p.FinancialTransactions)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinancialTransaction_Employee");

            builder.HasOne(d => d.TypeNavigation)
                .WithMany(p => p.FinancialTransactionTypes)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK_FinancialTransaction_ValueObject");
        }
    }
}
