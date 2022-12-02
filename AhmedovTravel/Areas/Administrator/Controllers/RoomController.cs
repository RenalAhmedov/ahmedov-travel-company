using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AhmedovTravel.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    public class RoomController : Controller
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService _roomService)
        {
            roomService = _roomService;
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> All()
        //{
        //    var model = await destinationService.GetAllAsync();

        //    return View(model);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Add()
        //{
        //    var model = new AddDestinationViewModel();

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Add(AddDestinationViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    try
        //    {
        //        await destinationService.AddDestinationAsync(model);

        //        return RedirectToAction(nameof(All));
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError("", "Something went wrong!");
        //        return View(model);
        //    }
        //}

        //[HttpGet]
        //[Authorize(Roles = ("Administrator"))]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    if ((await destinationService.Exists(id)) == false)
        //    {
        //        return RedirectToAction(nameof(All));
        //    }

        //    var destination = await destinationService.DestinationDetailsById(id);

        //    var model = new EditDestinationViewModel()
        //    {
        //        Id = id,
        //        Title = destination.Title,
        //        Town = destination.Town,
        //        ImageUrl = destination.ImageUrl,
        //        Price = destination.Price,
        //        Rating = destination.Rating,
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //[Authorize(Roles = ("Administrator"))]
        //public async Task<IActionResult> Edit(int id, EditDestinationViewModel model)
        //{
        //    if (id != model.Id)
        //    {
        //        ModelState.AddModelError("", "Something went wrong!");
        //        return RedirectToAction(nameof(All));
        //    }

        //    if ((await destinationService.Exists(model.Id)) == false)
        //    {
        //        ModelState.AddModelError("", "Destination doesn't exist");

        //        return View(model);
        //    }

        //    if (ModelState.IsValid == false)
        //    {
        //        return View(model);
        //    }

        //    await destinationService.Edit(model.Id, model);

        //    return RedirectToAction(nameof(All), new { model.Id });
        //}


        //[HttpGet]
        //[Authorize(Roles = ("Administrator"))]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if ((await destinationService.Exists(id)) == false)
        //    {
        //        return RedirectToAction(nameof(All));
        //    }

        //    var destination = await destinationService.DestinationDetailsById(id);
        //    var model = new AllDestinationsViewModel()
        //    {
        //        Title = destination.Title,
        //        Town = destination.Town,
        //        Price = destination.Price,
        //        ImageUrl = destination.ImageUrl,
        //        Rating = destination.Rating
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //[Authorize(Roles = ("Administrator"))]
        //public async Task<IActionResult> Delete(int id, AllDestinationsViewModel model)
        //{
        //    if ((await destinationService.Exists(id)) == false)
        //    {
        //        return RedirectToAction(nameof(All));
        //    }

        //    await destinationService.Delete(id);

        //    return RedirectToAction(nameof(All));
        //}
    }
}
