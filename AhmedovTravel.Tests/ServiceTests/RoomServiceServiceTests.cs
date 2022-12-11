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
            .UseInMemoryDatabase("RoomService")
            .Options;

            data = new ApplicationDbContext(contextOptions);

            repo = new Repository(data);
            roomServiceService = new RoomServiceService(repo);

            data.Database.EnsureDeleted();
            data.Database.EnsureCreated();
        }

        //[Test]
        //public async Task TestGetAll_RoomService()
        //{
        //    var expected = data.RoomServices.Where(d => d.IsActive).Count();

        //    await repo.SaveChangesAsync();

        //    var actual = roomServiceService.GetAllAsync().Result.Count();

        //    Assert.That(actual == expected);
        //}

        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}
