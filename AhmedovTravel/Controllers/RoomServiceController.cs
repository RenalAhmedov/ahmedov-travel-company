﻿using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedovTravel.Controllers
{
    public class RoomServiceController : Controller
    {
        private readonly IRoomServiceService roomService;

        public RoomServiceController(IRoomServiceService _roomService)
        {
            roomService = _roomService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await roomService.GetAllAsync();

            return View("All", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int roomServiceId)
        {
            try
            {
                var userId = User.Id();
                await roomService.AddRoomServiceToCollectionAsync(roomServiceId, userId);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> ShowRoomServiceCollection()
        {
            var userId = User.Id();
            var model = await roomService.ShowRoomServicetCollectionAsync(userId);

            return View("Mine", model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int roomServiceId)
        {
            var userId = User.Id();

            await roomService.RemoveRoomServiceFromCollectionAsync(roomServiceId, userId);

            return RedirectToAction(nameof(ShowRoomServiceCollection));
        }
    }
}