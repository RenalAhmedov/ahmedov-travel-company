using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Hotel;
using AhmedovTravel.Core.Models.Room;
using AhmedovTravel.Core.Services;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using AhmedovTravel.Infrastrucutre.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomService = AhmedovTravel.Core.Services.RoomService;

namespace AhmedovTravel.Tests.ServiceTests
{
    public class RoomServiceTests
    {
        private IRepository repo;
        private IRoomService roomService;
        private ApplicationDbContext data;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("HotelDb")
            .Options;

            data = new ApplicationDbContext(contextOptions);

            repo = new Repository(data);
            roomService = new RoomService(repo);

            data.Database.EnsureDeleted();
            data.Database.EnsureCreated();
        }

        [Test]
        public async Task TestAdd_Room()
        {
            var oldCount = await repo.AllReadonly<Room>().CountAsync();

            await roomService.AddRoomAsync(new AddRoomViewModel()
            {
                Persons = 1,
                ImageUrl = "asd1sd12414asa",
                PricePerNight = 50,
                RoomTypeId = 1
            });

            var afterAdding = await repo.AllReadonly<Room>().CountAsync();

            Assert.That(afterAdding, Is.EqualTo(oldCount + 1));
        }

        [Test]
        public async Task TestEdit_Room()
        {
            await repo.AddAsync(new Room() // try using only the repo methods for the collection tests! TODO
            {
                Persons = 1,
                ImageUrl = "asd1sd12414asa",
                PricePerNight = 50,
                RoomTypeId = 1
            });
            await repo.SaveChangesAsync();

            await roomService.Edit(1, new EditRoomViewModel()
            {
                Persons = 2,
                ImageUrl = "asd1sd12414asa",
                PricePerNight = 50,
                RoomTypeId = 1
            });

            var dbHotel = await repo.GetByIdAsync<Room>(1);

            Assert.That(dbHotel.Persons, Is.EqualTo(2));
        }

        [Test]
        public void TestEditThrowsNull_Room()
        {
            Assert.ThrowsAsync<NullReferenceException>(()
                 => roomService.Edit(1, new EditRoomViewModel()
                 {
                     Persons = 2,
                     ImageUrl = "",
                     PricePerNight = 50 ,
                     RoomTypeId = 1
                 }));
        }

        [Test]
        public async Task TestDelete_Room()
        {
            var startCount = 0;

            await repo.AddAsync(new Room()
            {
                Persons = 1,
                ImageUrl = "asd1sd12414asa",
                PricePerNight = 50,
                RoomTypeId = 1,
                IsActive = true
            });

            await roomService.Delete(1);

            var afterDelete = await repo.AllReadonly<Hotel>().Where(e => e.IsActive).CountAsync();

            Assert.That(startCount, Is.EqualTo(afterDelete));
        }

        [Test]
        public void TestDeleteThrowsNull_Room()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await roomService.Delete(88);
            });
        }

        [Test]
        public async Task TestGetAll_Room()
        {
            var expected = data.Rooms.Where(d => d.IsActive).Count();

            await repo.AddAsync(new Room() // try using only the repo methods for the collection tests! TODO
            {
                Persons = 1,
                ImageUrl = "asd1sd12414asa",
                PricePerNight = 50,
                RoomTypeId = 1,
                IsActive = true,
                IsChosen = false
            });
            await repo.SaveChangesAsync();

            var actual = roomService.GetAllAsync().Result.Count();

            Assert.That(actual > expected);
        }

        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}
