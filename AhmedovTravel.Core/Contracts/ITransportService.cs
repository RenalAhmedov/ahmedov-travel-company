using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Core.Models.Transport;

namespace AhmedovTravel.Core.Contracts
{
    public interface ITransportService
    {
        Task<IEnumerable<TransportViewModel>> GetAllAsync();
        Task AddDestinationToCollectionAsync(int transportId, string userId);
        Task<IEnumerable<TransportViewModel>> ShowTransportCollectionAsync(string userId);

        Task<TransportViewModel> TransportDetailsById(int id);
    }
}
