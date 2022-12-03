using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedovTravel.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService _roomService)
        {
            roomService = _roomService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await roomService.GetAllAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int roomId)
        {
            try
            {
                var userId = User.Id();
                await roomService.AddRoomToCollectionAsync(roomId, userId);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(ShowRoomCollection));
        }

        [HttpGet]
        public async Task<IActionResult> ShowRoomCollection()
        {
            var userId = User.Id();
            var model = await roomService.ShowRoomCollectionAsync(userId);

            return View("Mine", model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int roomId)
        {
            var userId = User.Id();

            await roomService.RemoveRoomFromCollectionAsync(roomId, userId);

            return RedirectToAction(nameof(ShowRoomCollection));
        }
    }
}
