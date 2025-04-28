using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerce.Repository;
using ECommerce.Models.Orders;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IRepository<Order> _orderRepo;

        public AdminController(IRepository<Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<IActionResult> AllOrders()
        {
            var orders = await _orderRepo.GetAllAsync(includeProperties: "OrderItems,OrderItems.Product");
            return View(orders);
        }
    }
}
