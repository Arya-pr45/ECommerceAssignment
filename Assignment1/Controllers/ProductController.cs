using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult Manage()
        {
            ProductManage productManage = new() { Action = 'A' };
            return View(productManage);
        }

        [HttpPost]
        public IActionResult Manage(ProductManage product)
        {
            product.Action = 'M';
            return View(product);
        }
        [HttpPost]
        public IActionResult SaveProduct(ProductManage req)
        {
            if (ModelState.IsValid)
            {
                if (req.Action == 'A')
                {
                    _context.Products.Add(req);
                }
                else if (req.Action == 'M')
                {
                    var existingProduct = _context.Products.Find(req.ProductId);
                    if (existingProduct != null)
                    {
                        existingProduct.Name = req.Name;
                        existingProduct.Price = req.Price;
                        existingProduct.Category = req.Category;
                        existingProduct.Description = req.Description;
                        existingProduct.Stock = req.Stock;
                    }
                }
                else if (req.Action == 'D')
                {
                    var product = _context.Products.Find(req.ProductId);
                    if (product != null)
                    {
                        _context.Products.Remove(product);
                    }
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Manage");
        }


        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}