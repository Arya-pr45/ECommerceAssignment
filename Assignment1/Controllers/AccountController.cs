using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using ECommerce.Services;
using ECommerce.Services.Interfaces;

namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userService.Authenticate(username, password);
            if (user != null)
            {
                TempData["User"] = username;
                if (user.Role == "Admin")
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Dashboard", "Customer");
            }

            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string role)
        {
            var result = _userService.Register(username, password, role);
            if (result)
            {
                TempData["Message"] = "Registered successfully!";
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Username already exists.";
            return View();
        }

        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Login");
        }
    }
}
