using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.RoomService;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Core.Services
{
    public class RoomServiceService : IRoomServiceService
    {
        private readonly IRepository repo;

        public RoomServiceService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddRoomServiceToCollectionAsync(int roomServiceId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UserRoomServices)
                 .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("Invalid user ID");
            }

            if (user.UserRoomServices.Count == 1)
            {
                throw new ArgumentException("you can add only one roomservice to the watchlist.");
            }

            var roomService = await repo.All<Infrastructure.Data.Entities.RoomService>()
                 .FirstOrDefaultAsync(d => d.Id == roomServiceId);

            if (roomService == null)
            {
                throw new ArgumentException("Invalid RoomService ID");
            }

            if (!user.UserRoomServices.Any(d => d.Id == roomServiceId))
            {
                user.UserRoomServices.Add(new Infrastructure.Data.Entities.RoomService()
                {
                    PricePerPerson = roomService.PricePerPerson,
                    ImageUrl = roomService.ImageUrl,
                    Description = roomService.Description
                });
            }
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoomServiceViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Infrastructure.Data.Entities.RoomService>()
               .Where(c => c.IsActive && c.Id == 1 || c.Id == 2) 
               .OrderBy(t => t.Id)
               .Select(t => new RoomServiceViewModel()
               {
                   Id = t.Id,
                   PricePerPerson = t.PricePerPerson,
                   Description = t.Description,
                   ImageUrl = t.ImageUrl,
               })
               .ToListAsync();
        }

        public async Task RemoveRoomServiceFromCollectionAsync(int roomServiceId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UserRoomServices)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("Invalid user ID");
            }

            var roomService = user.UserRoomServices.FirstOrDefault(m => m.Id == roomServiceId);

            if (roomService != null)
            {
                roomService.IsActive = false;
                user.UserRoomServices.Remove(roomService);

                await repo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RoomServiceViewModel>> ShowRoomServicetCollectionAsync(string userId)
        {
            var user = await repo.All<User>()
                 .Include(u => u.UserRoomServices)
                 .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UserRoomServices
                .Select(d => new RoomServiceViewModel()
                {
                    Id = d.Id,
                    PricePerPerson = d.PricePerPerson,
                    Description = d.Description,
                    ImageUrl = d.ImageUrl,
                });
        }
    }
}
