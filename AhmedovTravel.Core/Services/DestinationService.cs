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

        /// <summary>
        /// Creates a new Destination async
        /// </summary>
        /// <param name="model">View model containing the new Destination data</param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds the destination to the user's collection 
        /// </summary>
        /// <param name="destinationId">Destination Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given User doesn't exist</exception>
        /// <exception cref="ArgumentException">Throws if the user's Destination collection has 1 or more destinations inside.</exception>
        /// <exception cref="NullReferenceException">Throws if the given Destination doesn't exist</exception>
        public async Task AddDestinationToCollectionAsync(int destinationId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UsersDestinations) 
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("Invalid user ID");
            }

            if (user.UsersDestinations.Count == 1)
            {
                throw new ArgumentException("you can add only one destination to the watchlist.");
            }

            var destination = await repo.All<Destination>()
                 .FirstOrDefaultAsync(d => d.Id == destinationId);

            if (destination == null)
            {
                throw new NullReferenceException("Invalid Destination ID");
            }

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

        /// <summary>
        /// Sets a given Destination from Active to Inactive
        /// </summary>
        /// <param name="destinationId">Destination Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given Destination doesn't exist.</exception>
        public async Task Delete(int destinationId)
        {
            var destination = await repo.GetByIdAsync<Destination>(destinationId);

            if (destination == null)
            {
                throw new NullReferenceException();
            }

            destination.IsActive = false;

            await repo.SaveChangesAsync();
        }


        /// <summary>
        /// Gets Id and Name for all active departments in the database
        /// </summary>
        /// <returns><AllDestinationsViewModel></returns>
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

        /// <summary>
        /// Edits existing Destination
        /// </summary>
        /// <param name="model">Model with the Edit data</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given Department doesn't exist</exception>
        public async Task Edit(int destinationId, EditDestinationViewModel model)
        {
            var destination = await repo.GetByIdAsync<Destination>(destinationId);

            if (destination == null)
            {
                throw new NullReferenceException();
            }


            destination.Title = model.Title;
            destination.Town = model.Town;
            destination.ImageUrl = model.ImageUrl;
            destination.Rating = model.Rating;
            destination.Price = model.Price;

            await repo.SaveChangesAsync();
        }



        /// <summary>
        /// Checks if the destination exists and the status is IsActive
        /// </summary>
        /// <returns><Bool></returns>
        public async Task<bool> Exists(int id)
        {
            return await repo.AllReadonly<Destination>()
              .AnyAsync(h => h.Id == id && h.IsActive);
        }

        /// <summary>
        /// Gets all active destinations in the database
        /// </summary>
        /// <returns>IEnumerable<AllDestinationsViewModel> destinations</returns>
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


        /// <summary>
        /// Removes a given destination from the user's collection
        /// </summary>
        /// <param name="destinationId">Destination Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given Destination doesn't exist.</exception>
        public async Task RemoveDestinationFromCollectionAsync(int destinationId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UsersDestinations)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("Invalid user ID");
            }

            var destination = user.UsersDestinations.FirstOrDefault(m => m.DestinationId == destinationId);

            if (destination != null)
            {
                user.UsersDestinations.Remove(destination);

                await repo.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Shows the user's destination collection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given User doesn't exist.</exception>
        public async Task<IEnumerable<MineDestinationsViewModel>> ShowDestinationCollectionAsync(string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UsersDestinations)
                .ThenInclude(u => u.Destination)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("Invalid user ID");
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
