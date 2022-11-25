using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Core.Models.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhmedovTravel.Core.Contracts
{
    public interface ITransportService
    {
        Task<IEnumerable<TransportViewModel>> GetAllAsync();
        Task<IEnumerable<TransportViewModel>> AddTransportToWatchlist(string userId);
    }
}
