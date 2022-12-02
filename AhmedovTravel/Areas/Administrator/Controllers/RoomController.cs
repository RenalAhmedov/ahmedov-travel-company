using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AhmedovTravel.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
