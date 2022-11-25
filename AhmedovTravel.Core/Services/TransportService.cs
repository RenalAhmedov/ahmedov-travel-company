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

        public Task<IEnumerable<TransportViewModel>> ShowTransportCollectionAsync(string transportId)
        {
            throw new NotImplementedException();
        }
    }
}
