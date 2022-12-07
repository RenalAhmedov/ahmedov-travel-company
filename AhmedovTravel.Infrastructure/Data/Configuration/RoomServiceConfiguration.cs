using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedovTravel.Infrastructure.Data.Configuration
{
    internal class RoomServiceConfiguration : IEntityTypeConfiguration<RoomService>
    {
        public void Configure(EntityTypeBuilder<RoomService> builder)
        {
            builder.HasData(new RoomService()
            {
                Id = 1,
                PricePerPerson = 15,
                Description = "A Small Pizza, with small french fries and a bottle of Coca-Cola 350ml.",
                ImageUrl = "https://app.lazizpizzaa.com/storage/app/public/product/2022-01-21-61ea8774799d1.png"
            },
            new RoomService()
            {
                Id = 2,
                PricePerPerson = 20,
                Description = "A Beef Burger, with medium french fries, and a Coca-Cola 250ml",
                ImageUrl = "https://media.istockphoto.com/id/1344002306/photo/delicious-cheeseburger-with-cola-and-potato-fries-on-the-white-background-fast-food-concept.jpg?s=612x612&w=0&k=20&c=B8kZWz6zqmB11e4bIYt5rJ0U9aQ21AfZGgvT_JPIxqA="
            });

            builder.Property(c => c.IsActive)
              .HasDefaultValue(true);
        }
    }
}
