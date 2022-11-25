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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ShowDestinationCollection()
        {
            var userId = User.Id();
            var model = await transportService.ShowTransportCollectionAsync(userId);

            return View("Mine", model);
        }
    }
}
