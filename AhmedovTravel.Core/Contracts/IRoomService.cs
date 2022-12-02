﻿using AhmedovTravel.Core.Models.Room;
using AhmedovTravel.Infrastructure.Data.Entities;

namespace AhmedovTravel.Core.Contracts
{
    public interface IRoomService
    {
        //Task AddHotelAsync(AddHotelViewModel model);
        Task<IEnumerable<RoomType>> GetRoomTypes();
        Task<IEnumerable<RoomViewModel>> GetAllAsync();
        //Task AddHotelToCollectionAsync(int hotelId, string userId);
        //Task<IEnumerable<HotelViewModel>> ShowHotelCollectionAsync(string userId);
        //Task RemoveHotelFromCollectionAsync(int hotelId, string userId);
        //Task Edit(int hotelId, EditHotelViewModel model);
        //Task<bool> Exists(int id);
        //Task Delete(int hotelId);
        //Task<HotelViewModel> HotelDetailsById(int id);
    }
}
