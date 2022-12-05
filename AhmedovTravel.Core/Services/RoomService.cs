using AhmedovTravel.Core.Contracts;
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

        public async Task AddRoomToCollectionAsync(int roomId, string userId)
        {
            var user = await repo.All<User>()
                 .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var room = await repo.All<Room>()
                .Include(rt => rt.RoomType)
                 .FirstOrDefaultAsync(d => d.Id == roomId); // check!!! is chosen

            if (room == null)
            {
                throw new ArgumentException("Invalid Room ID");
            }

            if (!user.UserRooms.Any(d => d.Id == roomId))
            {
                user.UserRooms.Add(new Room()
                {
                    Persons = room.Persons,
                    PricePerNight = room.PricePerNight,
                    ImageUrl = room.ImageUrl,
                    RoomType = room.RoomType, // check
                    IsChosen = true //check
                });
            }
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
              .Where(c => c.IsActive == true && c.IsChosen == false)
              .Include(rt => rt.RoomType) // check
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

        public async Task RemoveRoomFromCollectionAsync(int roomId, string userId)
        {
            var user = await repo.All<User>()
               .Include(u => u.UserRooms)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var room = user.UserRooms.FirstOrDefault(m => m.Id == roomId);

            if (room != null)
            {
                room.IsChosen = true; // done it true and in hotel service aswell so it doesn't show in collection 
                user.UserRooms.Remove(room);

                await repo.SaveChangesAsync();
            }
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

        public async Task<IEnumerable<RoomViewModel>> ShowRoomCollectionAsync(string userId)
        {
            var user = await repo.All<User>()
               .Include(u => u.UserRooms)
               .ThenInclude(u => u.RoomType) // check
               .FirstOrDefaultAsync(u => u.Id == userId); //put where ischosen = false;

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UserRooms
                .Select(d => new RoomViewModel()
                {
                    Id = d.Id,
                    Persons = d.Persons,
                    PricePerNight = d.PricePerNight,
                    ImageUrl = d.ImageUrl,
                    RoomType = d.RoomType != null ? d.RoomType.Name : null // CHECK
                });
        }
    }
}
