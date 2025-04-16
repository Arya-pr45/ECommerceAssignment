using Microsoft.AspNetCore.Mvc;
using Assignment1.Models;
using Assignment1.Data;
using Assignment1.Services.Interfaces;
using Assignment1.Data;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        // Index action to display all products or filter by category
        public IActionResult Index(string category)
        {
            // Retrieve all products from the repository
            var products = _productRepository.GetAll();

            if (!string.IsNullOrEmpty(category))
            {
                products = _productRepository.GetProductsByCategory(category);
            }

            return View(products);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)  
            {
                _productRepository.Add(product);  
                return RedirectToAction("Index");  
            }

            return View(product);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.Find(id);
            if (product == null)
            {
                return NotFound();  
            }

            return View(product);  
        }
    }
}
