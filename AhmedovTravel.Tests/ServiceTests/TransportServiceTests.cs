using AhmedovTravel.Core.Contracts;
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
        public async Task TestAddToCollection_Transport()
        {
            await repo.AddAsync(new User()
            {
                UserName = "Testing",
                Email = "testingDestination@mail.com",
                IsActive = true
            });
            await repo.SaveChangesAsync();

            await repo.AddAsync(new Transport()
            {
                TransportType = "Bus",
                ImageUrl = "BusImage123",
                IsActive = true,
                IsChosen = false
            });
            await repo.SaveChangesAsync();

            var actualTransportId = await data.Transports.FirstAsync();
            var actualUserId = await data.Users.FirstAsync();

            await transportService.AddTransportToCollectionAsync(actualTransportId.Id, actualUserId.Id);

            var afterAdding = await repo.AllReadonly<User>().Include(uh => uh.UserTransport).FirstAsync();

            Assert.That(afterAdding.UserTransport.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestAddToCollectionThrowsNullExceptionWhenUserIdIsNull_Transport()
        {
            Assert.ThrowsAsync<NullReferenceException>(()
                 => transportService.AddTransportToCollectionAsync(1, ""));
        }
        [Test]
        public async Task TestAddToCollectionThrowsNullExceptionWhenTransportIdIsNull_Transport()
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
                 => transportService.AddTransportToCollectionAsync(77, actualUserId.Id));
        }

        [Test]
        public async Task TestGetAll_Transport()
        {
            var expected = data.Transports.Where(d => d.IsActive).Count();

            await repo.AddAsync(new Transport() 
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

        [Test]
        public async Task TestRemoveFromCollection_Transport()
        {
            var startCount = await repo.AllReadonly<Transport>().Where(e => e.IsActive).CountAsync();

            await repo.AddAsync(new User()
            {
                UserName = "Testing",
                Email = "testingDestination@mail.com",
                IsActive = true
            });
            await repo.SaveChangesAsync();

            await repo.AddAsync(new Transport()
            {
                TransportType = "Bus",
                ImageUrl = "BusImage123",
                IsActive = true,
                IsChosen = false
            });
            await repo.SaveChangesAsync();


            var actualTransportId = await data.Transports.FirstAsync();
            var actualUserId = await data.Users.FirstAsync();

            await transportService.RemoveTransportFromCollectionAsync(actualTransportId.Id,actualUserId.Id);

            var afterRemove = await repo.AllReadonly<User>().Include(uh => uh.UserTransport).FirstAsync();

            Assert.That(startCount, Is.EqualTo(afterRemove.UserTransport.Count));
        }

        [Test]
        public void TestRemoveFromCollectionThrowsNullExceptionWhenUserIdIsNull_Transport()
        {
            Assert.ThrowsAsync<NullReferenceException>(()
                 => transportService.RemoveTransportFromCollectionAsync(1, ""));
        }

        [Test]
        public async Task TestShowCollection_Transport()
        {
            await repo.AddAsync(new User()
            {
                UserName = "Testing",
                Email = "testingDestination@mail.com",
                IsActive = true
            });
            await repo.SaveChangesAsync();

            var actualUserId = await data.Users.FirstAsync();
            var actualRoomService = await data.Transports.FirstAsync();

            await transportService.AddTransportToCollectionAsync(actualRoomService.Id, actualUserId.Id);
            await repo.SaveChangesAsync();

            var actualTransport = await transportService.ShowTransportCollectionAsync(actualUserId.Id);
            await repo.SaveChangesAsync();
            var transport = actualTransport.FirstOrDefault();

            Assert.That(transport.TransportType, Is.EqualTo("Bus"));
            await repo.SaveChangesAsync();
        }



        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}
