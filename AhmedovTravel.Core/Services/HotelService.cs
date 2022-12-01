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
    }
}
