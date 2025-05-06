// Controllers/OrderController.cs
using System.Security.Claims;
using ECommerce.Interfaces;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = await _orderService.GetOrdersForUserAsync(userId);
            return View(viewModel);
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var success = await _orderService.CreateOrderAsync(userId, address);

            if (!success)
            {
                ModelState.AddModelError("", "Cart is empty or order could not be created.");
                return View("Checkout", address);
            }

            TempData["success"] = "Order Created Successfully";
            return RedirectToAction("Index", "Product");
        }
    }
}
