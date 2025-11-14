using HotelBookingApp.Models;
using HotelBookingApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBookingApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;
        private readonly IRoomService _roomService;

        public BookingController(
            IBookingService bookingService,
            IUserService userService,
            IRoomService roomService)
        {
            _bookingService = bookingService;
            _userService = userService;
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(bookings);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(booking);
            }

            var result = await _bookingService.CreateBookingAsync(booking);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);

                await LoadDropdowns();
                return View(booking);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            await LoadDropdowns();
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(booking);
            }

            var result = await _bookingService.UpdateBookingAsync(booking);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                await LoadDropdowns();
                return View(booking);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdowns()
        {
            var users = await _userService.GetAllUsers();
            var rooms = await _roomService.GetAllRoomsAsync();

            ViewBag.Users = new SelectList(users, "Id", "FirstName");
            ViewBag.Rooms = new SelectList(rooms, "Id", "Name");
        }
    }
}
