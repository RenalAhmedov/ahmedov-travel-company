using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Extensions;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Security.Claims;

namespace AhmedovTravel.Controllers
{
    [Authorize]
    public class DestinationController : Controller
    {
        private readonly IDestinationService destinationService;

        public DestinationController(IDestinationService _destinationService)
        {
            destinationService = _destinationService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await destinationService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddDestinationViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDestinationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await destinationService.AddDestinationAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int destinationId)
        {
            try
            {
                var userId = User.Id();
                await destinationService.AddDestinationToCollectionAsync(destinationId, userId);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> ShowDestinationCollection()
        {
            var userId = User.Id();
            var model = await destinationService.ShowDestinationCollectionAsync(userId);

            return View("Mine", model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await destinationService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var destination = await destinationService.DestinationDetailsById(id); //check

            var model = new EditDestinationViewModel()
            {
                Id = id,
                Title = destination.Title,
                Town = destination.Town,
                ImageUrl = destination.ImageUrl,
                Price = destination.Price,
                Rating = destination.Rating,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditDestinationViewModel model)
        {
            if (id != model.Id)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return RedirectToAction(nameof(All));
            }

            if ((await destinationService.Exists(model.Id)) == false)
            {
                ModelState.AddModelError("", "Destination doesn't exist");

                return View(model);
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await destinationService.Edit(model.Id, model);

            return RedirectToAction(nameof(All), new { model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int destinationId)
        {
            var userId = User.Id();

            await destinationService.RemoveDestinationFromCollectionAsync(destinationId, userId);

            return RedirectToAction(nameof(ShowDestinationCollection));
        }
    }
}
