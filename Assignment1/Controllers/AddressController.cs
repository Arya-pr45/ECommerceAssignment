using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var addresses = await _addressService.GetAddressesByUserIdAsync(userId);
                return View(addresses);
            }
            catch
            {
                ViewBag.Error = "Unable to load addresses.";
                return View(new List<Address>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    address.CustomerId = userId;
                    await _addressService.AddAddressAsync(address);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to add address.");
                }
            }
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> SetDefault(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _addressService.SetDefaultAddressAsync(id, userId);
            return RedirectToAction("Index");
        }
    }
}