using AhmedovTravel.Core.Models.Room;
using AhmedovTravel.Infrastructure.Data.Entities;

namespace AhmedovTravel.Core.Contracts
{
    public interface IRoomService
    {
        Task AddRoomAsync(AddRoomViewModel model);
        Task<IEnumerable<RoomType>> GetRoomTypes();
        Task<IEnumerable<RoomViewModel>> GetAllAsync();
        Task AddRoomToCollectionAsync(int roomId, string userId);
        Task<IEnumerable<RoomViewModel>> ShowRoomCollectionAsync(string userId);
        //Task RemoveHotelFromCollectionAsync(int hotelId, string userId);
        Task Edit(int roomId, EditRoomViewModel model);
        Task<bool> Exists(int id);
        Task Delete(int roomId);
        Task<RoomViewModel> RoomDetailsById(int id);
    }
}
