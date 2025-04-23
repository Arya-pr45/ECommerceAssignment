using Microsoft.AspNetCore.Mvc;
using ECommerce.Models.Products;
using Microsoft.AspNetCore.Authorization;
using ECommerce.Repository;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string category)
        {
            var products = string.IsNullOrEmpty(category)
                ? await _productRepository.GetAllAsync()
                : await _productRepository.FindAsync(p => p.Category == category);

            return View(products);
        }
        [Authorize(Roles ="Admin")]
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
        public async Task<IActionResult> SaveProduct(ProductManage req)
        {
            if (!ModelState.IsValid)
                return View("Manage");

            if (req.Action == 'A')
            {
                var newProduct = new Product
                {
                    Name = req.Name,
                    Price = req.Price,
                    Category = req.Category,
                    Description = req.Description,
                    Stock = req.Stock,
                    ImageUrl = req.ImageUrl
                };

                await _productRepository.AddAsync(newProduct);
            }
            else if (req.Action == 'M')
            {
                var existingProduct = await _productRepository.GetByIdAsync(req.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.Name = req.Name;
                    existingProduct.Price = req.Price;
                    existingProduct.Category = req.Category;
                    existingProduct.Description = req.Description;
                    existingProduct.Stock = req.Stock;
                    existingProduct.ImageUrl = req.ImageUrl;

                    _productRepository.Update(existingProduct);
                }
            }
            else if (req.Action == 'D')
            {
                var product = await _productRepository.GetByIdAsync(req.ProductId);
                if (product != null)
                {
                    _productRepository.Remove(product);
                }
            }

            await _productRepository.SaveAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                await _productRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                _productRepository.Remove(product);
                await _productRepository.SaveAsync();
            }
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
