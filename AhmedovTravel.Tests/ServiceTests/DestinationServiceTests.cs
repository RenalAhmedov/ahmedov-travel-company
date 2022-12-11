using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Core.Services;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using AhmedovTravel.Infrastrucutre.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using System;

namespace AhmedovTravel.Tests.ServiceTests
{
    public class DestinationServiceTests
    {
        private IRepository repo;
        private IDestinationService destinationService;
        private ApplicationDbContext data;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("DestinationDB")
            .Options;

            data = new ApplicationDbContext(contextOptions);

            repo = new Repository(data);
            destinationService = new DestinationService(repo);

            data.Database.EnsureDeleted();
            data.Database.EnsureCreated();
        }

        [Test]
        public async Task TestAdd_Destination()
        {
            var oldCount = await repo.AllReadonly<Destination>().CountAsync();

            await destinationService.AddDestinationAsync(new AddDestinationViewModel()
            {
                Title = "Laplandiq",
                Town = "Ahtopol",
                ImageUrl = "sadasdasdasdasd1231",
                Rating = 4,
                Price = 444
            });

            var afterAdding = await repo.AllReadonly<Destination>().CountAsync();

            Assert.That(afterAdding, Is.EqualTo(oldCount + 1));
        }

        [Test]
        public async Task TestEdit_Destination()
        {
            await repo.AddAsync(new Destination()
            {
                Id = 1,
                Title = "",
                ImageUrl = "",
                Town = "",
                Rating = 5,
                Price = 500
            });
            await repo.SaveChangesAsync();

            await destinationService.Edit(1, new EditDestinationViewModel()
            {
                Id = 1,
                Title = "",
                ImageUrl = "",
                Town = "Svishtov",
                Rating = 6,
                Price = 666
            });
            var dbDestination = await repo.GetByIdAsync<Destination>(1);

            Assert.That(dbDestination.Town, Is.EqualTo("Svishtov"));
        }

        [Test]
        public void TestEditThrowsNull_Destination()
        {
            Assert.ThrowsAsync<NullReferenceException>(()
                 => destinationService.Edit(1, new EditDestinationViewModel()
                 {
                     Id = 88,
                     Title = "asdasdasda",
                     Town = "asfdafsdfasdf",
                     ImageUrl = "asdsaasdasd112",
                     Rating = 5,
                     Price = 1
                 }));
        }

        [Test]
        public async Task TestDelete_Destination()
        {
            var startCount = 0;

            await repo.AddAsync(new Destination()
            {
                Id = 1,
                Title = "asdaasdasasdsdadsa",
                ImageUrl = "d12d1d1dasdasdasdas",
                Town = "asdasda",
                Rating = 5,
                Price = 500,
                IsActive = true
            });

            await destinationService.Delete(1);

            var afterDelete = await repo.AllReadonly<Destination>().Where(e => e.IsActive).CountAsync();

            Assert.That(startCount, Is.EqualTo(afterDelete));
        }

        [Test]
        public void TestDeleteThrowsNull_Destination()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await destinationService.Delete(88);
            });
        }

        [Test]
        public async Task TestGetAll_Destination()
        {
            var expected = data.Destinations.Where(d => d.IsActive).Count();

            var actual = destinationService.GetAllAsync().Result.Count();

            Assert.That(actual, Is.EqualTo(expected));
        }


        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}
