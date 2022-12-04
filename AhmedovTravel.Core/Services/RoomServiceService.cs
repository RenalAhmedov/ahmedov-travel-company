using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Infrastructure.Data.Common;

namespace AhmedovTravel.Core.Services
{
    public class RoomServiceService : IRoomServiceService
    {
        private readonly IRepository repo;

        public RoomServiceService(IRepository _repo)
        {
            repo = _repo;
        }
    }
}
