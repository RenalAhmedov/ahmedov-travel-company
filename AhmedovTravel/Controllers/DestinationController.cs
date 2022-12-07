using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            return RedirectToAction(nameof(ShowDestinationCollection));
        }

        [HttpGet]
        public async Task<IActionResult> ShowDestinationCollection()
        {
            var userId = User.Id();
            var model = await destinationService.ShowDestinationCollectionAsync(userId);

            return View("Mine", model);
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
