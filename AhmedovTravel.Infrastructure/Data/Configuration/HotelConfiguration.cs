using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedovTravel.Infrastructure.Data.Configuration
{
    internal class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {

            builder.HasOne(r => r.Room)
               .WithMany(hr => hr.HotelRooms)
               .HasForeignKey(ri => ri.RoomId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(d => d.RoomId)
             .IsRequired(false);

            builder.Property(tc => tc.IsActive)
               .HasDefaultValue(true);

        }
    }
}
