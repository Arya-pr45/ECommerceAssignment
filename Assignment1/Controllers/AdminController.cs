using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using ECommerce.Services;
using System.Collections.Generic;
using ECommerce.Services.Interfaces;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        // Constructor to inject dependencies
        public AdminController(IProductService productService, IOrderService orderService, IUserService userService)
        {
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
        }

        // Admin Dashboard View
        public IActionResult Index()
        {
            return View();
        }

        // Manage products - Admin can add new products or view/edit existing ones
        [HttpGet]
        public IActionResult ManageInventory()
        {
            var products = _productService.GetAllProducts();  // Get all products for admin view
            return View(products);
        }

        // Add a new product to inventory
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product); // Add product to inventory
                TempData["Message"] = "Product added successfully!";
                return RedirectToAction("ManageInventory");
            }

            return View(product); // If validation fails, return to the same view with model state errors
        }

        // Edit an existing product
        [HttpGet]
        public IActionResult EditProduct(int productId)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                TempData["Error"] = "Product not found!";
                return RedirectToAction("ManageInventory");
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                TempData["Message"] = "Product updated successfully!";
                return RedirectToAction("ManageInventory");
            }

            return View(product); // Return to the edit view with validation errors
        }

        // View All Orders for Admin
        [HttpGet]
        public IActionResult ViewAllOrders()
        {
            var orders = _orderService.GetAllOrders();  // Get all orders for admin
            return View(orders);
        }

        // Manage Users - View all users for Admin
        [HttpGet]
        public IActionResult ManageUsers()
        {
            var users = _userService.GetAllUsers();  // Get all users for admin view
            return View(users);
        }

        // View Order details
        [HttpGet]
        public IActionResult OrderDetails(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null)
            {
                TempData["Error"] = "Order not found!";
                return RedirectToAction("ViewAllOrders");
            }

            return View(order); // Return order details view
        }

        // Change order status (example: Pending to Shipped)
        [HttpPost]
        public IActionResult ChangeOrderStatus(int orderId, OrderStatus status)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order != null)
            {
                order.Status = status;
                _orderService.UpdateOrder(order);  // Update the order with the new status
                TempData["Message"] = $"Order status updated to {status}.";
                return RedirectToAction("ViewAllOrders");
            }

            TempData["Error"] = "Order not found!";
            return RedirectToAction("ViewAllOrders");
        }
    }
}
