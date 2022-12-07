using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedovTravel.Infrastructure.Data.Configuration
{
    internal class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    { 
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasData(new RoomType()
            {
                Id = 1,
                Name = "Single"
            },
            new RoomType()
            {
                Id = 2,
                Name = "Double"
            });
        }
    }
}
