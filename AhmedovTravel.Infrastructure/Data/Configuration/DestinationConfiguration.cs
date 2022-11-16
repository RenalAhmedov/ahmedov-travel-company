using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedovTravel.Infrastructure.Data.Configuration
{
    internal class DestinationConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            builder.Property(d => d.TownId)
               .IsRequired(false);

            builder.Property(d => d.IsActive)
               .HasDefaultValue(true);

            builder.Property(d => d.IsChosen)
               .HasDefaultValue(false);
        }
    }
}
