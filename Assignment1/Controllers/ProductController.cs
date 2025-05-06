using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerce.Interfaces;
using ECommerce.Models.Products;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string category)
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                if (!string.IsNullOrEmpty(category))
                    products = products.Where(p => p.Category == category);

                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products.");
                return View("Error", "Something went wrong while loading products.");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null) return NotFound();
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading product details for ID: {id}");
                return View("Error", "Unable to load product details.");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading admin dashboard.");
                return View("Error", "Unable to load admin dashboard.");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            try
            {
                await _productService.AddProductAsync(product);
                TempData["success"] = "Product created successfully!";
                return RedirectToAction("AdminDashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product.");
                ModelState.AddModelError("", "An error occurred while creating the product.");
                return View(product);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null) return NotFound();
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading edit page for product ID {id}");
                return View("Error", "Unable to load product for editing.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            try
            {
                await _productService.UpdateProductAsync(product);
                TempData["success"] = "Product updated successfully!";
                return RedirectToAction("AdminDashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product ID {product.ProductId}");
                ModelState.AddModelError("", "Failed to update product.");
                return View(product);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null) return NotFound();
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading delete page for product ID {id}");
                return View("Error", "Unable to load product for deletion.");
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                TempData["success"] = "Product deleted successfully!";
                return RedirectToAction("AdminDashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product ID {id}");
                return View("Error", "Failed to delete product.");
            }
        }
    }
}
