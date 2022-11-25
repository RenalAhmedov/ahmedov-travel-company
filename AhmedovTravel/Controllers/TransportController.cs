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
        public async Task<IActionResult> AddToWatchlist()
        {
            var userId = User.Id();
            var model = await transportService.AddTransportToWatchlist(userId);

            return View(model);
        }
    }
}
