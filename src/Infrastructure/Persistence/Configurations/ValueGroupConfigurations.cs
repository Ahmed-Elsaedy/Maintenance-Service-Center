using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElarabyCA.Infrastructure.Persistence.Configurations
{
    public class ValueGroupConfigurations : IEntityTypeConfiguration<ValueGroup>
    {
        public void Configure(EntityTypeBuilder<ValueGroup> builder)
        {
            builder.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Group).HasMaxLength(450);

            builder.Property(e => e.CreatedBy).HasMaxLength(450);

            builder.Property(e => e.LastModified).HasMaxLength(450);

            builder.Property(e => e.LastModifiedBy).HasMaxLength(450);

            builder.Property(e => e.Value).HasMaxLength(450);
        }
    }
}
