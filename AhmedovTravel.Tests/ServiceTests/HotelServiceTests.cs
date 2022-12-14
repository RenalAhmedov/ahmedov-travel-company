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
        public async Task TestAddToCollection_Hotel()
        {
            await repo.AddAsync(new User()
            {
                UserName = "Testing",
                Email = "testingDestination@mail.com",
                IsActive = true
            });
            await repo.SaveChangesAsync();

            await repo.AddAsync(new Hotel()
            {
                Name = "renalHotel",
                ImageUrl = "asdad12",
                Description = "HotelTest",
                HotelRating = 5,
                IsActive = true,
                IsChosen = false
            });
            await repo.SaveChangesAsync();

            var actualHotelId = await data.Hotels.FirstAsync();
            var actualUserId = await data.Users.FirstAsync();

            await hotelService.AddHotelToCollectionAsync(actualHotelId.Id, actualUserId.Id);

            var afterAdding = await repo.AllReadonly<User>().Include(uh => uh.UserHotels).FirstAsync();

            Assert.That(afterAdding.UserHotels.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestAddToCollectionThrowsNullExceptionWhenUserIdIsNull_Hotel()
        {
            Assert.ThrowsAsync<NullReferenceException>(()
                 => hotelService.AddHotelToCollectionAsync(1, ""));
        }

        [Test]
        public async Task TestAddToCollectionThrowsNullExceptionWhenHotelIdIsNull_Hotel()
        {
            await repo.AddAsync(new User()
            {
                UserName = "Testing",
                Email = "testingDestination@mail.com",
                IsActive = true
            });
            await repo.SaveChangesAsync();

            var actualUserId = await data.Users.FirstAsync();

            Assert.ThrowsAsync<NullReferenceException>(()
                 => hotelService.AddHotelToCollectionAsync(77, actualUserId.Id));
        }

        [Test]
        public async Task TestExists_Hotel()
        {
            await repo.AddAsync(new Hotel()
            {
                Name = "renalHotel",
                ImageUrl = "asdad12",
                Description = "HotelTest",
                HotelRating = 5,
                IsActive = true,
                IsChosen = false
            });
            await repo.SaveChangesAsync();

            var actual = await data.Hotels.FirstAsync();

            var expected = await hotelService.Exists(actual.Id);

            Assert.IsTrue(expected);
        }

        [Test]
        public async Task TestDetailsById_Hotel()
        {
            await repo.AddAsync(new Hotel()
            {
                Name = "renalHotel",
                ImageUrl = "asdad12",
                Description = "HotelTest",
                HotelRating = 5,
                IsActive = true,
                IsChosen = false
            });
            await repo.SaveChangesAsync();

            var expected = await data.Hotels.FirstAsync();

            var actual = await hotelService.HotelDetailsById(expected.Id);

            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
        }

        [Test]
        public async Task TestEdit_Hotel()
        {
            await repo.AddAsync(new Hotel()
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

            await repo.AddAsync(new Hotel()
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

        [Test]
        public async Task TestShowCollection_Hotel()
        {
            await repo.AddAsync(new User()
            {
                UserName = "Testing",
                Email = "testingDestination@mail.com",
                IsActive = true
            });
            await repo.SaveChangesAsync();

            await repo.AddAsync(new Hotel()
            {
                Name = "RenalHotel",
                Description = "RenalDescription",
                ImageUrl = "ASD1WSADA",
                HotelRating = 6,
                IsActive = true,
                IsChosen = false
            });
            await repo.SaveChangesAsync();

            var actualUserId = await data.Users.FirstAsync();
            var actualHotelId = await data.Hotels.FirstAsync();

            await hotelService.AddHotelToCollectionAsync(actualHotelId.Id, actualUserId.Id);
            await repo.SaveChangesAsync();

            var actualHotel = await hotelService.ShowHotelCollectionAsync(actualUserId.Id);
            await repo.SaveChangesAsync();
            var x = actualHotel.FirstOrDefault();

            Assert.That(x.Name, Is.EqualTo("RenalHotel"));
            await repo.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}
