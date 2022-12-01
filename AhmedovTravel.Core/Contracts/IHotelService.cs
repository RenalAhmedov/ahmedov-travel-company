using AhmedovTravel.Core.Models.Hotel;

namespace AhmedovTravel.Core.Contracts
{
    public interface IHotelService
    {
        Task AddHotelAsync(AddHotelViewModel model);
        Task<IEnumerable<HotelViewModel>> GetAllAsync();
        Task AddHotelToCollectionAsync(int hotelId, string userId);
        Task<IEnumerable<HotelViewModel>> ShowHotelCollectionAsync(string userId);
        Task RemoveHotelFromCollectionAsync(int hotelId, string userId);
        Task Edit(int hotelId, EditHotelViewModel model);
        Task<bool> Exists(int id);
        Task Delete(int hotelId);
        Task<HotelViewModel> HotelDetailsById(int id);
    }
}
