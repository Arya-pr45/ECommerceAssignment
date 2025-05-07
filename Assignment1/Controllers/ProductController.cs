using Microsoft.AspNetCore.Mvc;
using ECommerce.Models.Products;
using Microsoft.AspNetCore.Authorization;
using ECommerce.Interfaces;
using System;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productRepository)
        {
            _productService = productRepository;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string category)
        {
            try
            {
                var products = string.IsNullOrEmpty(category)
                    ? await _productService.GetAllProductsAsync()
                    : await _productService.FindByCategoryAsync(category);

                return View(products);
            }
            catch (Exception ex)
            {
                // Log exception (use a logging framework like Serilog or NLog in real projects)
                TempData["error"] = "Error fetching products.";
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Manage()
        {
            try
            {
                ProductManage productManage = new() { Action = 'A' };
                return View(productManage);
            }
            catch (Exception)
            {
                TempData["error"] = "Error loading product management page.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Manage(ProductManage product)
        {
            try
            {
                product.Action = 'M';
                return View(product);
            }
            catch (Exception)
            {
                TempData["error"] = "Error switching to modify mode.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductManage req)
        {
            if (!ModelState.IsValid)
                return View("Manage");

            try
            {
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

                    await _productService.AddProductAsync(newProduct);
                }
                else if (req.Action == 'M')
                {
                    var existingProduct = await _productService.GetProductByIdAsync(req.ProductId);
                    if (existingProduct != null)
                    {
                        existingProduct.Name = req.Name;
                        existingProduct.Price = req.Price;
                        existingProduct.Category = req.Category;
                        existingProduct.Description = req.Description;
                        existingProduct.Stock = req.Stock;
                        existingProduct.ImageUrl = req.ImageUrl;

                        await _productService.UpdateProductAsync(existingProduct);
                    }
                }
                else if (req.Action == 'D')
                {
                    var product = await _productService.GetProductByIdAsync(req.ProductId);
                    if (product != null)
                    {
                        _productService.Remove(product);
                    }
                }

                await _productService.SaveAsync();
                TempData["success"] = "Product saved successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Error saving product.";
                return View("Manage");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null) return NotFound();
                return View(product);
            }
            catch (Exception)
            {
                TempData["error"] = "Error loading product details.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null) return NotFound();
                return View(product);
            }
            catch (Exception)
            {
                TempData["error"] = "Error loading product for editing.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productService.Update(product);
                    await _productService.SaveAsync();
                    TempData["success"] = "Product updated successfully!";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception)
            {
                TempData["error"] = "Error updating product.";
                return RedirectToAction("Edit", new { id = product.ProductId });
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null) return NotFound();
                return View(product);
            }
            catch (Exception)
            {
                TempData["error"] = "Error loading product for deletion.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product != null)
                {
                    _productService.Remove(product);
                    await _productService.SaveAsync();
                }
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Error deleting product.";
                return RedirectToAction("Index");
            }
        }
    }
}
