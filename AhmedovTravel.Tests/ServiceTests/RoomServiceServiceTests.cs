using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Services;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using AhmedovTravel.Infrastrucutre.Data;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Tests.ServiceTests
{
    public class RoomServiceServiceTests
    {
        private IRepository repo;
        private IRoomServiceService roomServiceService;
        private ApplicationDbContext data;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("RoomServiceServiceDb")
            .Options;

            data = new ApplicationDbContext(contextOptions);

            repo = new Repository(data);
            roomServiceService = new RoomServiceService(repo);

            data.Database.EnsureDeleted();
            data.Database.EnsureCreated();
        }
        [Test]
        public async Task TestAddToCollection_RoomService()
        {
            await repo.AddAsync(new User()
            {
                UserName = "Testing",
                Email = "testingDestination@mail.com",
                IsActive = true
            });
            await repo.SaveChangesAsync();

            await repo.AddAsync(new AhmedovTravel.Infrastructure.Data.Entities.RoomService()
            {
                PricePerPerson = 40,
                Description = "Sweet and salty",
                ImageUrl = "BurgerPhoto"
            });
            await repo.SaveChangesAsync();

            var actualRoomServiceId = await data.RoomServices.FirstAsync();
            var actualUserId = await data.Users.FirstAsync();

            await roomServiceService.AddRoomServiceToCollectionAsync(actualRoomServiceId.Id, actualUserId.Id);

            var afterAdding = await repo.AllReadonly<User>().Include(uh => uh.UserRoomServices).FirstAsync();

            Assert.That(afterAdding.UserRoomServices.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestAddToCollectionThrowsNullExceptionWhenUserIdIsNull_RoomService()
        {
            Assert.ThrowsAsync<NullReferenceException>(()
                 => roomServiceService.AddRoomServiceToCollectionAsync(1, ""));
        }

        [Test]
        public async Task TestAddToCollectionThrowsNullExceptionWhenRoomServiceIdIsNull_RoomService()
        {
            //add transportr
            await repo.AddAsync(new User()
            {
                UserName = "Testing",
                Email = "testingDestination@mail.com",
                IsActive = true
            });
            await repo.SaveChangesAsync();

            var actualUserId = await data.Users.FirstAsync();

            Assert.ThrowsAsync<NullReferenceException>(()
                 => roomServiceService.AddRoomServiceToCollectionAsync(77, actualUserId.Id));
        }

        [Test]
        public async Task TestRemoveFromCollection_RoomService()
        {
            var startCount = await repo.AllReadonly<AhmedovTravel.Infrastructure.Data.Entities.RoomService>().Where(e => e.IsActive).CountAsync();

            await repo.AddAsync(new User()
            {
                UserName = "Testing",
                Email = "testingDestination@mail.com",
                IsActive = true
            });
            await repo.SaveChangesAsync();

            await repo.AddAsync(new AhmedovTravel.Infrastructure.Data.Entities.RoomService()
            {
                PricePerPerson = 40,
                Description = "Sweet and salty",
                ImageUrl = "BurgerPhoto"
            });
            await repo.SaveChangesAsync();


            var actualRoomServiceId = await data.RoomServices.FirstAsync();
            var actualUserId = await data.Users.FirstAsync();

            await roomServiceService.RemoveRoomServiceFromCollectionAsync(actualRoomServiceId.Id, actualUserId.Id);

            var afterRemove = await repo.AllReadonly<User>().Include(uh => uh.UserRoomServices).FirstAsync();

            Assert.That(startCount, Is.EqualTo(afterRemove.UserRoomServices.Count));
        }

        [Test]
        public void TestRemoveFromCollectionThrowsNullExceptionWhenUserIdIsNull_RoomService()
        {
            Assert.ThrowsAsync<NullReferenceException>(()
                 => roomServiceService.RemoveRoomServiceFromCollectionAsync(1, ""));
        }

        [Test]
        public async Task TestGetAll_RoomService()
        {
            var expected = data.RoomServices.Where(d => d.IsActive).Count();

            await repo.AddAsync(new AhmedovTravel.Infrastructure.Data.Entities.RoomService() // try using only the repo methods for the collection tests! TODO
            {
                PricePerPerson = 40,
                Description = "Sweet and salty",
                ImageUrl = "BurgerPhoto",
                IsActive = true
            });
            await repo.SaveChangesAsync();

            var actual = roomServiceService.GetAllAsync().Result.Count();

            Assert.That(actual > expected);
        }

        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}
