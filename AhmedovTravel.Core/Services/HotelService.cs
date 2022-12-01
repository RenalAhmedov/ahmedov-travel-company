using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Destination;
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

        public async Task AddHotelToCollectionAsync(int hotelId, string userId)
        {
            var user = await repo.All<User>()
                 .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var hotel = await repo.All<Hotel>()
                 .FirstOrDefaultAsync(d => d.Id == hotelId);

            if (hotel == null)
            {
                throw new ArgumentException("Invalid Hotel ID");
            }

            if (!user.UserHotels.Any(d => d.Id == hotelId))
            {
                user.UserHotels.Add(new Hotel()
                {
                    Name = hotel.Name,
                    Description = hotel.Description,
                    HotelRating = hotel.HotelRating,
                    ImageUrl = hotel.ImageUrl,
                });
            }
            await repo.SaveChangesAsync();
        }

        public async Task Delete(int hotelId)
        {
            var hotel = await repo.GetByIdAsync<Hotel>(hotelId);
            hotel.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task Edit(int hotelId, EditHotelViewModel model)
        {
            var hotel = await repo.GetByIdAsync<Hotel>(hotelId);

            hotel.Name = model.Name;
            hotel.Description = model.Description;
            hotel.HotelRating = model.HotelRating;
            hotel.ImageUrl = model.ImageUrl;

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await repo.AllReadonly<Hotel>()
            .AnyAsync(h => h.Id == id && h.IsActive);
        }

        public async Task<IEnumerable<HotelViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Hotel>()
              .Where(c => c.IsActive)
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
                user.UserHotels.Remove(hotel);

                await repo.SaveChangesAsync();
            }
        }

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
