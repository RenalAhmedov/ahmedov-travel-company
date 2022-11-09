using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedovTravel.Infrastructure.Data.Configuration
{
    internal class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasOne(t => t.Hotel)
                .WithMany(t => t.TownHotels)
                .HasForeignKey(t => t.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(tc => tc.IsActive)
               .HasDefaultValue(true);
        }
    }
}
