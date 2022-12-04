using AhmedovTravel.Core.Models.Transport;

namespace AhmedovTravel.Core.Contracts
{
    public interface ITransportService
    {
        Task<IEnumerable<TransportViewModel>> GetAllAsync();
        Task AddTransportToCollectionAsync(int transportId, string userId);
        Task<IEnumerable<TransportViewModel>> ShowTransportCollectionAsync(string userId);
        //Task<TransportViewModel> TransportDetailsById(int id);
        Task RemoveTransportFromCollectionAsync(int transportId, string userId);
        
    }
}
