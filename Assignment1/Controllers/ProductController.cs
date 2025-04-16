using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using ECommerce.Data;
using ECommerce.Services.Interfaces;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        //private readonly IRepository<Product> _productRepository;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
            //_productRepository = productRepository;
        }

        public IActionResult Index(string category)
        {
            var products = _context.Products.ToList(); 
            return View(products);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)  
            {
                //_productRepository.Add(product);  
                return RedirectToAction("Index");  
            }

            return View(product);
        }
        [HttpPost]
        public IActionResult SaveProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("AddProduct"); 
        }
        //public IActionResult Details(int id)
        //{
        //    //var product = _productRepository.Find(id);
        //    if (product == null)
        //    {
        //        return NotFound();  
        //    }

        //    return View(product);  
        //}
    }
}
