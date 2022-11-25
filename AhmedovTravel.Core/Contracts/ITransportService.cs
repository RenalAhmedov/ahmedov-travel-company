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
        Task<IEnumerable<TransportViewModel>> ShowTransportCollectionAsync(string transportId);
        Task<IEnumerable<TransportViewModel>> GetAllAsync();
    }
}
