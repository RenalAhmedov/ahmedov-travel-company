using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Hotel;
using AhmedovTravel.Core.Services;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using AhmedovTravel.Infrastrucutre.Data;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Tests.ServiceTests
{
    public class HotelServiceTests
    {
        private IRepository repo;
        private IHotelService hotelService;
        private ApplicationDbContext data;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("HotelDb")
            .Options;

            data = new ApplicationDbContext(contextOptions);

            repo = new Repository(data);
            hotelService = new HotelService(repo);

            data.Database.EnsureDeleted();
            data.Database.EnsureCreated();
        }

        [Test]
        public async Task TestAdd_Hotel()
        {
            var oldCount = await repo.AllReadonly<Hotel>().CountAsync();

            await hotelService.AddHotelAsync(new AddHotelViewModel()
            {
                Name = "RenalHotel",
                Description = "RenalDescription",
                ImageUrl = "ASD1WSADA",
                HotelRating = 6
            });

            var afterAdding = await repo.AllReadonly<Hotel>().CountAsync();

            Assert.That(afterAdding, Is.EqualTo(oldCount + 1));
        }

        [Test]
        public async Task TestEdit_Hotel()
        {
            await repo.AddAsync(new Hotel() // try using only the repo methods for the collection tests! TODO
            {
                Name = "RenalHotel",
                Description = "RenalDescription",
                ImageUrl = "ASD1WSADA",
                HotelRating = 6
            });
            await repo.SaveChangesAsync();

            await hotelService.Edit(1, new EditHotelViewModel()
            {
                Name = "EditedHotel",
                Description = "EditedDescription",
                ImageUrl = "ASD1WSADA",
                HotelRating = 7
            });

            var dbHotel = await repo.GetByIdAsync<Hotel>(1);

            Assert.That(dbHotel.Name, Is.EqualTo("EditedHotel"));
        }

        [Test]
        public void TestEditThrowsNull_Hotel()
        {
            Assert.ThrowsAsync<NullReferenceException>(()
                 => hotelService.Edit(1, new EditHotelViewModel()
                 {
                     Name = "",
                     Description = "",
                     ImageUrl = "",
                     HotelRating = 5
                 }));
        }

        [Test]
        public async Task TestDelete_Hotel()
        {
            var startCount = 0;

            await repo.AddAsync(new Hotel()
            {
                Name = "RenalHotel",
                Description = "RenalDescription",
                ImageUrl = "ASD1WSADA",
                HotelRating = 6,
                IsActive = true
            });

            await hotelService.Delete(1);

            var afterDelete = await repo.AllReadonly<Hotel>().Where(e => e.IsActive).CountAsync();

            Assert.That(startCount, Is.EqualTo(afterDelete));
        }

        [Test]
        public void TestDeleteThrowsNull_Hotel()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await hotelService.Delete(88);
            });
        }

        [Test]
        public async Task TestGetAll_Hotel()
        {
            var expected = data.Hotels.Where(d => d.IsActive).Count();

            await repo.AddAsync(new Hotel() // try using only the repo methods for the collection tests! TODO
            {
                Name = "RenalHotel",
                Description = "RenalDescription",
                ImageUrl = "ASD1WSADA",
                HotelRating = 6,
                IsActive = true,
                IsChosen = false
            });
            await repo.SaveChangesAsync();

            var actual = hotelService.GetAllAsync().Result.Count();

            Assert.That(actual > expected);
        }

        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}
