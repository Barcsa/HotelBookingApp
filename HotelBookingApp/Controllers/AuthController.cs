using HotelBookingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Login()
        {
            var users = await _userService.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> LoginConfirm(int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
                return RedirectToAction("Login");

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("IsAdmin", user.IsAdmin ? "True" : "False");
            HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
