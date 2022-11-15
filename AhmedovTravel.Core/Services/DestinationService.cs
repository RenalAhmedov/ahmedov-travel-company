using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Destination;
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
                Rating = model.Rating,
                Price = model.Price
            };
            await repo.AddAsync(destination);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllDestinationsViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Destination>()
               .OrderBy(d => d.Id)
               .Select(d => new AllDestinationsViewModel()
               {
                   Id = d.Id,
                   ImageUrl = d.ImageUrl,
                   Title = d.Title
               })
               .ToListAsync();
        }
    }
}
