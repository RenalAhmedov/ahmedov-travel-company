using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Hotel;
using AhmedovTravel.Core.Models.Room;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository repo;

        public RoomService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddRoomAsync(AddRoomViewModel model)
        {
            var room = new Room()
            {
                Persons = model.Persons,
                ImageUrl = model.ImageUrl,
                PricePerNight = model.PricePerNight,
                RoomTypeId = model.RoomTypeId,

            };
            await repo.AddAsync(room);
            await repo.SaveChangesAsync();
        }

        public async Task Delete(int roomId)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);
            room.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task Edit(int roomId, EditRoomViewModel model)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);

            room.Persons = model.Persons;
            room.ImageUrl = model.ImageUrl;
            room.PricePerNight = model.PricePerNight;
            room.RoomTypeId = model.RoomTypeId; //check!

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await repo.AllReadonly<Room>()
           .AnyAsync(h => h.Id == id && h.IsActive);
        }

        public async Task<IEnumerable<RoomViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Room>()
              .Where(c => c.IsActive == true)
              .Include(rt => rt.RoomType) // check
              /*              .Where(h => h.IsChosen == false)*/ //check
              .OrderBy(d => d.Id)
              .Select(d => new RoomViewModel()
              {
                  Id = d.Id,
                  Persons = d.Persons,
                  ImageUrl = d.ImageUrl,
                  PricePerNight = d.PricePerNight,
                  RoomType = d.RoomType.Name,
              })
              .ToListAsync();
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypes()
        {
            return await repo.All<RoomType>().ToListAsync();
        }

        public async Task<RoomViewModel> RoomDetailsById(int id)
        {
            return await repo.AllReadonly<Room>()
               .Where(h => h.IsActive)
               .Where(h => h.Id == id)
               .Select(h => new RoomViewModel()
               {
                   Id = id,
                   Persons = h.Persons,
                   ImageUrl = h.ImageUrl,
                   PricePerNight = h.PricePerNight,
                   RoomType = h.RoomType != null ? h.RoomType.Name : null //check
               })
               .FirstAsync();
        }
    }
}
