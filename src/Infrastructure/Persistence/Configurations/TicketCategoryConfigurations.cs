using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Infrastructure.Persistence.Configurations
{
    class TicketCategoryConfigurations : IEntityTypeConfiguration<TicketCategory>
    {
        public void Configure(EntityTypeBuilder<TicketCategory> builder)
        {
            builder.HasKey(e => e.Oid);

            builder.HasIndex(e => e.Gcrecord)
                .HasName("iGCRecord_TicketCategory");

            builder.HasIndex(e => e.Title)
                .HasName("iTitle_TicketCategory")
                .IsUnique();

            builder.Property(e => e.Oid).HasColumnName("OID");

            builder.Property(e => e.Gcrecord).HasColumnName("GCRecord");

            builder.Property(e => e.Title).HasMaxLength(100);
        }
    }
}
