using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Hotel;
using AhmedovTravel.Core.Models.Room;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository repo;

        public RoomService(IRepository _repo)
        {
            repo = _repo;
        }
        public async Task<IEnumerable<RoomViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Room>()
              .Where(c => c.IsActive == true)
              /*              .Where(h => h.IsChosen == false)*/ //check
              .OrderBy(d => d.Id)
              .Select(d => new RoomViewModel()
              {
                  Id = d.Id,
                  Persons = d.Persons,
                  ImageUrl = d.ImageUrl,
                  PricePerNight = d.PricePerNight,
                  RoomType = d.RoomType.Name,
              })
              .ToListAsync();
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypes()
        {
            return await repo.All<RoomType>().ToListAsync(); //check
        }
    }
}
