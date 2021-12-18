using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Oid).HasName("OID");

            builder.Property(e => e.Oid).HasColumnName("OID");

            builder.HasIndex(e => e.Gcrecord)
                .HasName("iGCRecord_Employee");

            builder.HasIndex(e => e.ObjectType)
                .HasName("iObjectType_Employee");

            builder.Property(e => e.Birthdate).HasColumnType("datetime");

            builder.Property(e => e.City).HasMaxLength(100);

            builder.Property(e => e.DateEnrolled).HasColumnType("datetime");

            builder.Property(e => e.DisplayName).HasMaxLength(100);

            builder.Property(e => e.FullName).HasMaxLength(100);

            builder.Property(e => e.Gcrecord).HasColumnName("GCRecord");

            builder.Property(e => e.IdentityNumber).HasMaxLength(100);

            builder.Property(e => e.PrimaryPhone).HasMaxLength(100);

            builder.Property(e => e.Province).HasMaxLength(100);

            builder.Property(e => e.SecondaryPhone).HasMaxLength(100);

            builder.Property(e => e.Street).HasMaxLength(100);

            builder.Property(e => e.Town).HasMaxLength(100);

            builder.Property(e => e.UserName).HasMaxLength(100);
        }
    }
}
