using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Services;
using AhmedovTravel.Infrastructure.Data.Common;
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
