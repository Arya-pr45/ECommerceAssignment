using System.Security.Claims;
using ECommerce.Models;
using ECommerce.Models.Carts;
using ECommerce.Models.Orders;
using ECommerce.Models.Products;
using ECommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Cart> _cartItemRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<OrderItem> _orderItemsRepo;

        public OrderController(IRepository<Cart> cartItemRepo, IRepository<Product> productRepo, IRepository<Order> orderRepo, IRepository<OrderItem> orderItemsRepo)
        {
            _cartItemRepo = cartItemRepo;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _orderItemsRepo = orderItemsRepo;
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Index()
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Order> orders = await _orderRepo.FindAsync(user => user.UserId == userId);
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var order in orders)
            {
                IEnumerable<OrderItem> items = await _orderItemsRepo.FindAsync(OrderItem => OrderItem.OrderId == order.OrderId);
                OrderView orderView = new();
                foreach (var item in items)
                {
                    var product = await _productRepo.GetByIdAsync(item.ProductId);
                    orderView.OrderDate = order.OrderDate;
                    orderView.Name = product.Name;
                    orderView.Price = product.Price;
                    orderView.Quantity = item.Quantity;
                    orderView.OrderId = order.OrderId;
                    orderView.Status = order.Status;
                    orderView.TotalAmount = order.TotalAmount;
                }
                orderViewModel.orderList.Add(orderView);
            }
            return View(orderViewModel);
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(Address address)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            IEnumerable<Cart> cartItems = await _cartItemRepo.FindAsync(c => c.UserId == userId);
            address.CustomerId = Convert.ToInt32(userId);
            if (!cartItems.Any())
                return BadRequest("Cart is empty.");
            decimal totalAmount = 0;
            foreach (var cartItem in cartItems)
            {
                var product = await _productRepo.GetByIdAsync(cartItem.ProductId);
                totalAmount += product.Price;
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                ShippingAddress = address,
                TotalAmount = totalAmount,
                OrderItems = new List<OrderItem>()
            };

            foreach (var cartItem in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product.Price
                });
            }

            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveAsync();

            foreach (var cartItem in cartItems)
            {
                _cartItemRepo.Remove(cartItem);
            }
            await _cartItemRepo.SaveAsync();
            TempData["success"] = "Order Created Successfully";
            return RedirectToAction("Index","Product");
        }

    }
}
