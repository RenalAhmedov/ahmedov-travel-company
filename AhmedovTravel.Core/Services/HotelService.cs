using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Hotel;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Core.Services
{
    public class HotelService : IHotelService
    {
        private readonly IRepository repo;

        public HotelService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Creates a new Hotel async
        /// </summary>
        /// <param name="model">View model containing the new Hotel data</param>
        /// <returns></returns>
        public async Task AddHotelAsync(AddHotelViewModel model)
        {
            var hotel = new Hotel()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                HotelRating = model.HotelRating,
            };
            await repo.AddAsync(hotel);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Adds the hotel to the user's collection 
        /// </summary>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given User doesn't exist</exception>
        /// <exception cref="ArgumentException">Throws if the user's Hotel collection has 1 or more destinations inside.</exception>
        /// <exception cref="NullReferenceException">Throws if the given Hotel doesn't exist</exception>
        public async Task AddHotelToCollectionAsync(int hotelId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UserHotels)
                 .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("Invalid user ID");
            }

            if (user.UserHotels.Count == 1)
            {
                throw new ArgumentException("you can add only one hotel to the watchlist.");
            }

            var hotel = await repo.All<Hotel>()
                 .FirstOrDefaultAsync(d => d.Id == hotelId);

            if (hotel == null)
            {
                throw new NullReferenceException("Invalid Hotel ID");
            }

            if (!user.UserHotels.Any(d => d.Id == hotelId))
            {
                user.UserHotels.Add(new Hotel()
                {
                    Name = hotel.Name,
                    Description = hotel.Description,
                    HotelRating = hotel.HotelRating,
                    ImageUrl = hotel.ImageUrl,
                    IsChosen = true 
                });
            }
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Sets a given Hotel from Active to Inactive
        /// </summary>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given Hotel doesn't exist.</exception>
        public async Task Delete(int hotelId)
        {
            var hotel = await repo.GetByIdAsync<Hotel>(hotelId);
            hotel.IsActive = false;

            await repo.SaveChangesAsync();
        }


        /// <summary>
        /// Edits existing Hotel
        /// </summary>
        /// <param name="model">Model with the Edit data</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given Hotel doesn't exist</exception>
        public async Task Edit(int hotelId, EditHotelViewModel model)
        {
            var hotel = await repo.GetByIdAsync<Hotel>(hotelId);

            hotel.Name = model.Name;
            hotel.Description = model.Description;
            hotel.HotelRating = model.HotelRating;
            hotel.ImageUrl = model.ImageUrl;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the Hotel exists and the status is IsActive
        /// </summary>
        /// <returns><Bool></returns>
        public async Task<bool> Exists(int id)
        {
            return await repo.AllReadonly<Hotel>()
            .AnyAsync(h => h.Id == id && h.IsActive);
        }

        /// <summary>
        /// Gets all active hotels in the database
        /// </summary>
        /// <returns>IEnumerable<HotelViewModel> hotels</returns>
        public async Task<IEnumerable<HotelViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Hotel>()
              .Where(c => c.IsActive == true)
              .Where(h => h.IsChosen == false) 
              .OrderBy(d => d.Id)
              .Select(d => new HotelViewModel()
              {
                  Id = d.Id,
                  Name = d.Name,
                  Description = d.Description,
                  ImageUrl = d.ImageUrl,
                  HotelRating = d.HotelRating,
              })
              .ToListAsync();
        }

        /// <summary>
        /// Gets Id and Name for all active Hotels in the database
        /// </summary>
        /// <returns><AllDestinationsViewModel></returns>
        public async Task<HotelViewModel> HotelDetailsById(int id)
        {
            return await repo.AllReadonly<Hotel>()
               .Where(h => h.IsActive)
               .Where(h => h.Id == id)
               .Select(h => new HotelViewModel()
               {
                   Id = id,
                   Description = h.Description,
                   Name = h.Name,
                   HotelRating = h.HotelRating,
                   ImageUrl = h.ImageUrl
               })
               .FirstAsync();
        }

        /// <summary>
        /// Removes a given hotel from the user's collection
        /// </summary>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given Hotel doesn't exist.</exception>
        public async Task RemoveHotelFromCollectionAsync(int hotelId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UserHotels)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var hotel = user.UserHotels.FirstOrDefault(m => m.Id == hotelId);

            if (hotel != null)
            {
                hotel.IsChosen = true;
                user.UserHotels.Remove(hotel);

                await repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Shows the user's hotel collection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given User doesn't exist.</exception>
        public async Task<IEnumerable<HotelViewModel>> ShowHotelCollectionAsync(string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UserHotels)
                .FirstOrDefaultAsync(u => u.Id == userId); 

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UserHotels
                .Select(d => new HotelViewModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    HotelRating = d.HotelRating,
                    ImageUrl = d.ImageUrl
                });
        }
    }
}
