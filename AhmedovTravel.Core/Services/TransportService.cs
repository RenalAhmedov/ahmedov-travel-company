using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Core.Models.Transport;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhmedovTravel.Core.Services
{
    public class TransportService : ITransportService
    {
        private readonly IRepository repo;

        public TransportService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<TransportViewModel>> AddTransportToWatchlist(string userId)
        {
            var user = await repo.All<User>()
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UserTransport
                .Select(t => new TransportViewModel()
                {
                    Id = t.Id,
                    TransportType = t.TransportType,
                    ImageUrl = t.ImageUrl,
                });
        }

        public async Task<IEnumerable<TransportViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Transport>()
                .Where(c => c.IsActive)
                .OrderBy(t => t.Id)
                .Select(t => new TransportViewModel()
                {
                    Id = t.Id,
                    TransportType = t.TransportType,
                    ImageUrl = t.ImageUrl,

                })
                .ToListAsync();
        }
    }
}
