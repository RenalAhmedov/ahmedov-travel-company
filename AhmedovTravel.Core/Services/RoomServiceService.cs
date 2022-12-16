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

        /// <summary>
        /// Adds the room service to the user's collection 
        /// </summary>
        /// <param name="roomServiceId">RoomService Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given User doesn't exist</exception>
        /// <exception cref="ArgumentException">Throws if the user's RoomService collection has 1 or more RoomServices inside.</exception>
        /// <exception cref="NullReferenceException">Throws if the given RoomService doesn't exist</exception>
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
                throw new NullReferenceException("Invalid RoomService ID");
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

        /// <summary>
        /// Gets all active RoomServices in the database
        /// </summary>
        /// <returns>IEnumerable<RoomServiceViewModel> RoomServices</returns>
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

        /// <summary>
        /// Removes a given room service from the user's collection
        /// </summary>
        /// <param name="roomServiceId">RoomService Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given RoomService doesn't exist.</exception>
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

        /// <summary>
        /// Shows the user's roomservice collection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given User doesn't exist.</exception>
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
