//using ECommerce.Data;
//using ECommerce.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace ECommerce.Controllers
//{
//    [Authorize(Roles = "Admin")]
//    public class AdminController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public AdminController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult ProductList()
//        {
//            var products = _context.Products.ToList();
//            return View(products);
//        }

//        [HttpPost]
//        public IActionResult MarkOutOfStock(int id)
//        {
//            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
//            if (product == null)
//                return NotFound();

//            product.IsInStock = false;
//            _context.SaveChanges();

//            return RedirectToAction("ProductList");
//        }

//        [HttpPost]
//        public IActionResult MarkInStock(int id)
//        {
//            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
//            if (product == null)
//                return NotFound();

//            product.IsInStock = true;
//            _context.SaveChanges();

//            return RedirectToAction("ProductList");
//        }
//        public IActionResult Dashboard()
//        {
//            var totalUsers = _context.Users.Count();
//            var totalOrders = _context.Orders.Count();
//            var totalProducts = _context.Products.Count();
//            var outOfStockCount = _context.Products.Count(p => !p.IsInStock);

//            var summary = new
//            {
//                Users = totalUsers,
//                Orders = totalOrders,
//                Products = totalProducts,
//                OutOfStock = outOfStockCount
//            };

//            ViewBag.Summary = summary;

//            return View();
//        }
//    }
//}
