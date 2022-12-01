using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedovTravel.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddHotelViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddHotelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await hotelService.AddHotelAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await hotelService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var hotel = await hotelService.HotelDetailsById(id);

            var model = new EditHotelViewModel()
            {
                Id = id,
                Name = hotel.Name,
                Description = hotel.Description,
                ImageUrl = hotel.ImageUrl,
                HotelRating = hotel.HotelRating,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> Edit(int id, EditHotelViewModel model)
        {
            if (id != model.Id)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return RedirectToAction(nameof(All));
            }

            if ((await hotelService.Exists(model.Id)) == false)
            {
                ModelState.AddModelError("", "Hotel doesn't exist");

                return View(model);
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await hotelService.Edit(model.Id, model);

            return RedirectToAction(nameof(All), new { model.Id });
        }


        [HttpGet]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await hotelService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var hotel = await hotelService.HotelDetailsById(id);
            var model = new HotelViewModel()
            {
                Name = hotel.Name,
                Description = hotel.Description,
                ImageUrl = hotel.ImageUrl,
                HotelRating = hotel.HotelRating,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> Delete(int id, HotelViewModel model)
        {
            if ((await hotelService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            await hotelService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
