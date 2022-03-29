using LocationCapture.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocationCapture.Persistence.EntityConfigurations
{
    public class PlasementConfiguration : IEntityTypeConfiguration<Placement>
    {
        public void Configure(EntityTypeBuilder<Placement> builder)
        {
            builder.Property(v => v.Vehicle)
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}
