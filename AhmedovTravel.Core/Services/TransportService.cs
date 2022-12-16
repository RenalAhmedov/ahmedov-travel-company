using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Transport;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Core.Services
{
    public class TransportService : ITransportService
    {
        private readonly IRepository repo;

        public TransportService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Adds the transport to the user's collection 
        /// </summary>
        /// <param name="transportId">Transport Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given User doesn't exist</exception>
        /// <exception cref="ArgumentException">Throws if the user's Transport collection has 1 or more transports inside.</exception>
        /// <exception cref="NullReferenceException">Throws if the given Transport doesn't exist</exception>
        public async Task AddTransportToCollectionAsync(int transportId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UserTransport)
                 .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("Invalid user ID");
            }

            if (user.UserTransport.Count == 1)
            {
                throw new ArgumentException("you can add only one transport to the watchlist.");
            }

            var transport = await repo.All<Transport>()
                 .FirstOrDefaultAsync(d => d.Id == transportId);

            if (transport == null)
            {
                throw new NullReferenceException("Invalid Transport ID");
            }

            if (!user.UserTransport.Any(d => d.Id == transportId))
            {
                user.UserTransport.Add(new Transport()
                {
                    TransportType = transport.TransportType,
                    ImageUrl = transport.ImageUrl
                  
                });
            }
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all active transports in the database
        /// </summary>
        /// <returns>IEnumerable<TransportViewModel> transports</returns>
        public async Task<IEnumerable<TransportViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Transport>()
                .Where(c => c.IsActive && c.Id == 1 || c.Id == 2)
                .OrderBy(t => t.Id)
                .Select(t => new TransportViewModel()
                {
                    Id = t.Id,
                    TransportType = t.TransportType,
                    ImageUrl = t.ImageUrl,

                })
                .ToListAsync();
        }

        /// <summary>
        /// Removes a given transport from the user's collection
        /// </summary>
        /// <param name="transportId">Transport Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given Transport doesn't exist.</exception>
        public async Task RemoveTransportFromCollectionAsync(int transportId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UserTransport)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("Invalid user ID");   
            }

            var transport = user.UserTransport.FirstOrDefault(m => m.Id == transportId);

            if (transport != null)
            {
                transport.IsActive = false;
                user.UserTransport.Remove(transport);
                
                await repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Shows the user's transport collection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given User doesn't exist.</exception>
        public async Task<IEnumerable<TransportViewModel>> ShowTransportCollectionAsync(string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UserTransport)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UserTransport
                .Select(d => new TransportViewModel()
                {
                    Id = d.Id,
                    TransportType = d.TransportType,
                    ImageUrl = d.ImageUrl,
                });
        }
    }
}
