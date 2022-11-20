using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Extensions;
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
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new AddDestinationViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AddDestinationViewModel model)
        {
            return RedirectToAction(nameof(All), new { id });
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
