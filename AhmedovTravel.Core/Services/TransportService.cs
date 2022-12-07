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

        public async Task AddTransportToCollectionAsync(int transportId, string userId)
        {
            var user = await repo.All<User>()
                 .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var transport = await repo.All<Transport>()
                 .FirstOrDefaultAsync(d => d.Id == transportId);

            if (transport == null)
            {
                throw new ArgumentException("Invalid Transport ID");
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

        public async Task RemoveTransportFromCollectionAsync(int transportId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UserTransport)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");   
            }

            var transport = user.UserTransport.FirstOrDefault(m => m.Id == transportId);

            if (transport != null)
            {
                transport.IsActive = false;
                user.UserTransport.Remove(transport);
                
                await repo.SaveChangesAsync();
            }
        }

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
