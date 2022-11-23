using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedovTravel.Infrastructure.Data.Configuration
{
    internal class TransportConfiguration : IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.HasData(new Transport()
            {
                Id = 1,
                TransportType = "Bus",
                ImageUrl = "https://media.istockphoto.com/id/492620954/photo/classical-red-bus.jpg?s=612x612&w=0&k=20&c=U2P9mlO8D7xZCYjRfifEkWxdUHp7JH7XPBn2dB1c9Qs="
            },
            new Transport()
            {
                Id = 2,
                TransportType = "Airplane",
                ImageUrl = "https://media.istockphoto.com/id/155439315/photo/passenger-airplane-flying-above-clouds-during-sunset.jpg?s=612x612&w=0&k=20&c=LJWadbs3B-jSGJBVy9s0f8gZMHi2NvWFXa3VJ2lFcL0="
            });

            builder.Property(c => c.IsActive)
              .HasDefaultValue(true);
        }
    }
}
