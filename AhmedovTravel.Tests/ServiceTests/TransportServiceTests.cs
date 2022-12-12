using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Hotel;
using AhmedovTravel.Core.Services;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using AhmedovTravel.Infrastrucutre.Data;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Tests.ServiceTests
{
    public class TransportServiceTests
    {
        private IRepository repo;
        private ITransportService transportService;
        private ApplicationDbContext data;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TransportDb")
            .Options;

            data = new ApplicationDbContext(contextOptions);

            repo = new Repository(data);
            transportService = new TransportService(repo);

            data.Database.EnsureDeleted();
            data.Database.EnsureCreated();
        }

        [Test]
        public async Task TestGetAll_Transport()
        {
            var expected = data.Transports.Where(d => d.IsActive).Count();

            await repo.AddAsync(new Transport() // try using only the repo methods for the collection tests! TODO
            {
                TransportType = "Bus",
                ImageUrl = "BusImage123",
                IsActive = true,
                IsChosen = false
            });
            await repo.SaveChangesAsync();

            var actual = transportService.GetAllAsync().Result.Count();

            Assert.That(actual > expected);
        }



        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}
