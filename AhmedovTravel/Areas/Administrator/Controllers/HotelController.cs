using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedovTravel.Areas.Administrator.Controllers
{
    public class HotelController : Controller
    {
        [Area("Administrator")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
