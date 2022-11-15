using AhmedovTravel.Core.Models.Destination;

namespace AhmedovTravel.Core.Contracts
{
    public interface IDestinationService
    {
        Task AddDestinationAsync(AddDestinationViewModel model);
        Task<IEnumerable<AllDestinationsViewModel>> GetAllAsync();
    }
}
