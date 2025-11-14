using HotelBookingApp.Models;
using HotelBookingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;

        public RoomController(IRoomService roomService, IBookingService bookingService)
        {
            _roomService = roomService;
            _bookingService = bookingService;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return View(rooms);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            if (!ModelState.IsValid)
                return View(room);

            await _roomService.CreateRoomAsync(room);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Room room)
        {
            if (!ModelState.IsValid)
                return View(room);

            await _roomService.UpdateRoomAsync(room);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomService.DeleteRoomAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Calendar(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            var bookings = await _bookingService.GetBookingsByRoomAsync(id);

            ViewBag.Room = room;
            return View(bookings);
        }
    
    }
}
