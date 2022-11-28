using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Core.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IRepository repo;

        public DestinationService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddDestinationAsync(AddDestinationViewModel model)
        {
            var destination = new Destination()
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                Town = model.Town,
                Rating = model.Rating,
                Price = model.Price
            };
            await repo.AddAsync(destination);
            await repo.SaveChangesAsync();
        }

        public async Task AddDestinationToCollectionAsync(int destinationId, string userId)
        {
            var user = await repo.All<User>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var destination = await repo.All<Destination>()
                 .FirstOrDefaultAsync(d => d.Id == destinationId);

            if (destination == null)
            {
                throw new ArgumentException("Invalid Destination ID");
            }

            if (destination.IsChosen == true)
            {
                throw new ArgumentException("You already have that destination chosen.");
            }

            destination.IsChosen = true;

            if (!user.UsersDestinations.Any(d => d.DestinationId == destinationId))
            {
                user.UsersDestinations.Add(new UserDestination()
                {
                    DestinationId = destination.Id,
                    UserId = user.Id,
                    Destination = destination,
                    User = user
                });
            }
            await repo.SaveChangesAsync();
        }

        public async Task Delete(int destinationId)
        {
            var destination = await repo.GetByIdAsync<Destination>(destinationId);
            destination.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task<AllDestinationsViewModel> DestinationDetailsById(int id)
        {
            return await repo.AllReadonly<Destination>()
                .Where(h => h.IsActive)
                .Where(h => h.Id == id)
                .Select(h => new AllDestinationsViewModel()
                {
                    Id = id,
                    ImageUrl = h.ImageUrl,        
                    Title = h.Title,
                    Town = h.Town,
                    Price = h.Price,
                    Rating = h.Rating

                })
                .FirstAsync();
        }

        public async Task Edit(int destinationId, EditDestinationViewModel model)
        {
            var destination = await repo.GetByIdAsync<Destination>(destinationId);

            destination.Title = model.Title;
            destination.Town = model.Town;
            destination.ImageUrl = model.ImageUrl;
            destination.Rating = model.Rating;
            destination.Price = model.Price;

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await repo.AllReadonly<Destination>()
              .AnyAsync(h => h.Id == id && h.IsActive);
        }

        public async Task<IEnumerable<AllDestinationsViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Destination>()
               .Where(c => c.IsActive)
               .OrderBy(d => d.Id)
               .Select(d => new AllDestinationsViewModel()
               {
                   Id = d.Id,
                   ImageUrl = d.ImageUrl,
                   Town = d.Town,
                   Price = d.Price,
                   Rating = d.Rating,
                   Title = d.Title
               })
               .ToListAsync();
        }

        public async Task RemoveDestinationFromCollectionAsync(int destinationId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UsersDestinations)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var destinationSelected = await repo.All<Destination>()
               .FirstOrDefaultAsync(d => d.Id == destinationId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var destination = user.UsersDestinations.FirstOrDefault(m => m.DestinationId == destinationId);

            if (destination != null)
            {
                destinationSelected.IsChosen = false;
                user.UsersDestinations.Remove(destination);

                await repo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MineDestinationsViewModel>> ShowDestinationCollectionAsync(string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UsersDestinations)
                .ThenInclude(u => u.Destination)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UsersDestinations
                .Select(d => new MineDestinationsViewModel()
                {
                    Id = d.Destination.Id,
                    Title = d.Destination.Title,
                    Town = d.Destination.Town,
                    ImageUrl = d.Destination.ImageUrl,
                    Rating = d.Destination.Rating,
                    Price = d.Destination.Price
                });
        }
    }
}
