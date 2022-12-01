using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Infrastructure.Data.Common;

namespace AhmedovTravel.Core.Services
{
    public class HotelService : IHotelService
    {
        private readonly IRepository repo;

        public HotelService(IRepository _repo)
        {
            repo = _repo;
        }
    }
}
