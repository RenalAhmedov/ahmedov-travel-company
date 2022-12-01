using AhmedovTravel.Core.Models.Hotel;

namespace AhmedovTravel.Core.Contracts
{
    public interface IHotelService
    {
        Task AddHotelAsync(AddHotelViewModel model);
        Task<IEnumerable<HotelViewModel>> GetAllAsync();
        //Task AddDestinationToCollectionAsync(int destinationId, string userId);
        //Task<IEnumerable<MineDestinationsViewModel>> ShowDestinationCollectionAsync(string userId);
        //Task RemoveDestinationFromCollectionAsync(int destinationId, string userId);
        //Task Edit(int destinationId, EditDestinationViewModel model);
        //Task<bool> Exists(int id);
        //Task Delete(int destinationId);
        //Task<AllDestinationsViewModel> DestinationDetailsById(int id);
    }
}
