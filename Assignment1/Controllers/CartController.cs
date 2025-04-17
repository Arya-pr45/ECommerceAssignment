using Microsoft.AspNetCore.Mvc;
using ECommerce.Services;
using ECommerce.Models;
using CartS.Services;

namespace ECommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> Index(int customerId)
        {
            var cart = await _cartService.GetCartAsync(customerId);

            if (cart == null)
            {
                return NotFound("Cart not found for this customer.");
            }

            return View(cart);
        }
    }
}
