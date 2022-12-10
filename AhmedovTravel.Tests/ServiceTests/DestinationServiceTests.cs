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
        private IRepository mockRepo;
        private IDestinationService destinationService;
        private ApplicationDbContext applicationDbContext;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("DestinationDB")
            .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            mockRepo = new Repository(applicationDbContext);
            destinationService = new DestinationService(mockRepo);

            applicationDbContext.Database.EnsureDeleted();

            applicationDbContext.Database.EnsureCreated();
        }

        //[Test]
        //public async Task Test_Add_Destination()
        //{
        //    var mockRepo = new Repository(applicationDbContext);
        //    destinationService = new DestinationService(repo);

        //    var oldCount = await repo.AllReadonly<Destination>().Where(e => e.IsActive).CountAsync();

        //    await destinationService.AddDestinationAsync(new AddDestinationViewModel()
        //    {
        //        Title = "Laplandiq",
        //        Town = "Ahtopol",
        //        ImageUrl = "sadasdasdasdasd1231",
        //        Rating = 4,
        //        Price = 444
        //    });

        //    var afterAdding = await repo.AllReadonly<Destination>().Where(e => e.IsActive).CountAsync();

        //    Assert.That(afterAdding, Is.EqualTo(oldCount + 1));
        //}

        [Test]
        public async Task Test_Destination_Edit()
        {
            await mockRepo.AddAsync(new Destination()
            {
                Id = 1,
                Title = "",
                ImageUrl = "",
                Town = "",
                Rating = 5,
                Price = 500
            });
            await mockRepo.SaveChangesAsync();

            await destinationService.Edit(1, new EditDestinationViewModel()
            {
                Id = 1,
                Title = "",
                ImageUrl = "",
                Town = "Svishtov",
                Rating = 6,
                Price = 666
            });
            var dbDestination = await mockRepo.GetByIdAsync<Destination>(1);

            Assert.That(dbDestination.Town, Is.EqualTo("Svishtov"));
        }

        [Test]
        public async Task DeleteAsync_RemovesDestination()
        {
            var startCount = 0;

            await mockRepo.AddAsync(new Destination()
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

            var afterDelete = await mockRepo.AllReadonly<Destination>().Where(e => e.IsActive).CountAsync();

            Assert.That(startCount, Is.EqualTo(afterDelete));
        }
        


        [TearDown]
        public void TearDown()
        {
            applicationDbContext.Dispose();
        }
    }
}
