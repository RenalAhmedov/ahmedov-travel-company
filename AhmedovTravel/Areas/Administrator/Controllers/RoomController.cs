using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedovTravel.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddRoomViewModel()
            {
                RoomTypes = await roomService.GetRoomTypes()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await roomService.AddRoomAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await roomService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var room = await roomService.RoomDetailsById(id);

            var model = new Core.Models.Room.EditRoomViewModel()
            {
                Id = id,
                Persons = room.Persons,
                PricePerNight = room.PricePerNight,
                ImageUrl = room.ImageUrl,
                RoomTypes = await roomService.GetRoomTypes() // check
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> Edit(int id, EditRoomViewModel model)
        {
            if (id != model.Id)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return RedirectToAction(nameof(All));
            }

            if ((await roomService.Exists(model.Id)) == false)
            {
                ModelState.AddModelError("", "Destination doesn't exist");

                return View(model);
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await roomService.Edit(model.Id, model);

            return RedirectToAction(nameof(All), new { model.Id });
        }


        [HttpGet]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await roomService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var room = await roomService.RoomDetailsById(id);
            var model = new RoomViewModel()
            {
                Persons = room.Persons,
                ImageUrl = room.ImageUrl,
                PricePerNight = room.PricePerNight,
                RoomType = room.RoomType //check
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> Delete(int id, RoomViewModel model)
        {
            if ((await roomService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            await roomService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
