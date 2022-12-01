using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedovTravel.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
