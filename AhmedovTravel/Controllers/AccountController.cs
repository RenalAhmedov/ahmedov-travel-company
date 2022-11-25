using Microsoft.AspNetCore.Mvc;

namespace AhmedovTravel.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
