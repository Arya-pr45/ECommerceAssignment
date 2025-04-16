using Microsoft.AspNetCore.Mvc;
using Assignment1.Models;
using Assignment1.Data;
using System.Linq;
using Assignment1.Data;
using Assignment1.Models;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            var orders = _orderRepository.GetOrdersSortedByDate();
            return View(orders);
        }
    }
}
