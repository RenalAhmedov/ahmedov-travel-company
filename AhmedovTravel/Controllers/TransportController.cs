using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AhmedovTravel.Controllers
{
    public class TransportController : Controller
    {

        private readonly ITransportService transportService;

        public TransportController(ITransportService _transportService)
        {
            transportService = _transportService;
        }

        public async Task<IActionResult> All()
        {
            var model = await transportService.GetAllAsync();

            return View("All", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int transportId)
        {
            try
            {
                var userId = User.Id();
                await transportService.AddDestinationToCollectionAsync(transportId, userId);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> ShowTransportCollection()
        {
            var userId = User.Id();
            var model = await transportService.ShowTransportCollectionAsync(userId);

            return View("Mine", model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int transportId)
        {
            var userId = User.Id();

            await transportService.RemoveTransportFromCollectionAsync(transportId, userId);

            return RedirectToAction(nameof(ShowTransportCollection));
        }
    }
}
