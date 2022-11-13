using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedovTravel.Controllers
{
    [Authorize]
    public class DestinationController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = new DestinationsQueryModel();

            return View();
        }

        public async Task<IActionResult> Mine()
        {
            var model = new DestinationsQueryModel();

            return View();
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(DestinationModel model)
        {
            int id = 1;

            return RedirectToAction(nameof(All), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new DestinationModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, DestinationModel model)
        {
            return RedirectToAction(nameof(All), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            return RedirectToAction(nameof(All));
        }


    }
}
