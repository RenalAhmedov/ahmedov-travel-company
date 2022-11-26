using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Services;
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
        public async Task<IActionResult> ShowTransportCollectionById(int userId)
        {
            var model = await transportService.TransportDetailsById(userId);

            return View("Mine", model);
        }
    }
}
