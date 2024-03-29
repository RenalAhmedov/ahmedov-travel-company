﻿using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Room;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository repo;

        public RoomService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Creates a new Room async
        /// </summary>
        /// <param name="model">View model containing the new Room data</param>
        /// <returns></returns>
        public async Task AddRoomAsync(AddRoomViewModel model)
        {
            var room = new Room()
            {
                Persons = model.Persons,
                ImageUrl = model.ImageUrl,
                PricePerNight = model.PricePerNight,
                RoomTypeId = model.RoomTypeId,

            };
            await repo.AddAsync(room);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Adds the room to the user's collection 
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given User doesn't exist</exception>
        /// <exception cref="ArgumentException">Throws if the user's Room collection has 1 or more rooms inside.</exception>
        /// <exception cref="NullReferenceException">Throws if the given Room doesn't exist</exception>
        public async Task AddRoomToCollectionAsync(int roomId, string userId)
        {
            var user = await repo.All<User>()
                .Include(u => u.UserRooms)
                 .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("Invalid user ID");
            }

            if (user.UserRooms.Count == 1)
            {
                throw new ArgumentException("you can add only one room to the watchlist.");
            }

            var room = await repo.All<Room>()
                .Include(rt => rt.RoomType)
                 .FirstOrDefaultAsync(d => d.Id == roomId);

            if (room == null)
            {
                throw new NullReferenceException("Invalid Room ID");
            }

            if (!user.UserRooms.Any(d => d.Id == roomId))
            {
                user.UserRooms.Add(new Room()
                {
                    Persons = room.Persons,
                    PricePerNight = room.PricePerNight,
                    ImageUrl = room.ImageUrl,
                    RoomType = room.RoomType,
                    IsChosen = true 
                });
            }
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Sets a given Room from Active to Inactive
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given Room doesn't exist.</exception>
        public async Task Delete(int roomId)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);
            room.IsActive = false;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Edits existing Room
        /// </summary>
        /// <param name="model">Model with the Edit data</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given Room doesn't exist</exception>
        public async Task Edit(int roomId, EditRoomViewModel model)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);

            room.Persons = model.Persons;
            room.ImageUrl = model.ImageUrl;
            room.PricePerNight = model.PricePerNight;
            room.RoomTypeId = model.RoomTypeId; 

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the Room exists and the status is IsActive
        /// </summary>
        /// <returns><Bool></returns>
        public async Task<bool> Exists(int id)
        {
            return await repo.AllReadonly<Room>()
           .AnyAsync(h => h.Id == id && h.IsActive);
        }

        /// <summary>
        /// Gets all active rooms in the database
        /// </summary>
        /// <returns>IEnumerable<RoomViewModel> rooms</returns>
        public async Task<IEnumerable<RoomViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Room>()
              .Where(c => c.IsActive == true && c.IsChosen == false)
              .Include(rt => rt.RoomType) 
              .OrderBy(d => d.Id)
              .Select(d => new RoomViewModel()
              {
                  Id = d.Id,
                  Persons = d.Persons,
                  ImageUrl = d.ImageUrl,
                  PricePerNight = d.PricePerNight,
                  RoomType = d.RoomType.Name,
              })
              .ToListAsync();
        }

        /// <summary>
        /// Gets all the roomtypes
        /// </summary>
        /// <returns><Bool></returns>
        public async Task<IEnumerable<RoomType>> GetRoomTypes()
        {
            return await repo.All<RoomType>().ToListAsync();
        }

        /// <summary>
        /// Removes a given room from the user's collection
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given Room doesn't exist.</exception>
        public async Task RemoveRoomFromCollectionAsync(int roomId, string userId)
        {
            var user = await repo.All<User>()
               .Include(u => u.UserRooms)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var room = user.UserRooms.FirstOrDefault(m => m.Id == roomId);

            if (room != null)
            {
                room.IsChosen = true; 
                user.UserRooms.Remove(room);

                await repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets Id for all active Rooms in the database
        /// </summary>
        /// <returns><RoomViewModel></returns>
        public async Task<RoomViewModel> RoomDetailsById(int id)
        {
            return await repo.AllReadonly<Room>()
               .Where(h => h.IsActive)
               .Where(h => h.Id == id)
               .Select(h => new RoomViewModel()
               {
                   Id = id,
                   Persons = h.Persons,
                   ImageUrl = h.ImageUrl,
                   PricePerNight = h.PricePerNight,
                   RoomType = h.RoomType != null ? h.RoomType.Name : null 
               })
               .FirstAsync();
        }


        /// <summary>
        /// Shows the user's rooms collection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Throws if the given User doesn't exist.</exception>
        public async Task<IEnumerable<RoomViewModel>> ShowRoomCollectionAsync(string userId)
        {
            var user = await repo.All<User>()
               .Include(u => u.UserRooms)
               .ThenInclude(u => u.RoomType) 
               .FirstOrDefaultAsync(u => u.Id == userId); 

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UserRooms
                .Select(d => new RoomViewModel()
                {
                    Id = d.Id,
                    Persons = d.Persons,
                    PricePerNight = d.PricePerNight,
                    ImageUrl = d.ImageUrl,
                    RoomType = d.RoomType != null ? d.RoomType.Name : null 
                });
        }
    }
}
