﻿using AhmedovTravel.Core.Constants;
using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedovTravel.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        private readonly IHotelService hotelService;

        public HotelController(IHotelService _hotelService)
        {
            hotelService = _hotelService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await hotelService.GetAllAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int hotelId)
        {
            try
            {
                var userId = User.Id();
                await hotelService.AddHotelToCollectionAsync(hotelId, userId);
                TempData[MessageConstants.SuccessMessage] = "Successfully added hotel to watchlist.";
            }
            catch (Exception)
            {
                TempData[MessageConstants.ErrorMessage] = "You have a chosen hotel already.";
            }

            return RedirectToAction(nameof(ShowHotelCollection)); 
        }

        [HttpGet]
        public async Task<IActionResult> ShowHotelCollection()
        {
            var userId = User.Id();
            var model = await hotelService.ShowHotelCollectionAsync(userId);

            return View("Mine", model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int hotelId)
        {
            var userId = User.Id();

            await hotelService.RemoveHotelFromCollectionAsync(hotelId, userId);

            return RedirectToAction(nameof(ShowHotelCollection));
        }
    }
}
