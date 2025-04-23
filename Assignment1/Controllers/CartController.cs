using Microsoft.AspNetCore.Mvc;
using ECommerce.Repository;
using ECommerce.Models.Carts;
using ECommerce.Models.Products;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace ECommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IRepository<Cart> _cartItemRepo;
        private readonly IRepository<Product> _productRepo;

        public CartController(IRepository<Cart> cartItemRepo, IRepository<Product> productRepo)
        {
            _cartItemRepo = cartItemRepo;
            _productRepo = productRepo;
        }
        public async Task<IActionResult> Index()
        {
            CartViewModel cartViewModel = new CartViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Cart> cartItems = await _cartItemRepo.FindAsync(c => c.UserId == userId);
            List<CartItem> cartItemList = new();
            foreach (var cartItem in cartItems)
            {
                var product = await _productRepo.GetByIdAsync(cartItem.ProductId);
                CartItem cartItemObj = new()
                {
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = cartItem.Quantity,
                    ProductId = cartItem.ProductId
                };
                cartItemList.Add(cartItemObj);
            }
            cartViewModel.cartList = cartItemList;
            return View(cartViewModel);
        }
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = _productRepo.Find(productId);
            if (product == null) return NotFound();

            var existingCartItem = (await _cartItemRepo.FindAsync(c =>
                c.ProductId == productId && c.UserId == userId)).FirstOrDefault();

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
                _cartItemRepo.Update(existingCartItem);
            }
            else
            {
                var cartItem = new Cart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = 1,
                    Product = product
                };
                await _cartItemRepo.AddAsync(cartItem);
            }

            await _cartItemRepo.SaveAsync();
            TempData["success"] = "Added to Cart Successfully";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItem = (await _cartItemRepo.FindAsync(c =>
                c.ProductId == productId && c.UserId == userId)).FirstOrDefault();

            if (cartItem != null)
            {
                _cartItemRepo.Remove(cartItem);
                await _cartItemRepo.SaveAsync();
            }
            TempData["success"] = "Removed From Cart";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItem = (await _cartItemRepo.FindAsync(c => c.ProductId == productId && c.UserId == userId)).FirstOrDefault();

            if (cartItem != null)
            {
                cartItem.Quantity++;
                _cartItemRepo.Update(cartItem);
                await _cartItemRepo.SaveAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItem = (await _cartItemRepo.FindAsync(c => c.ProductId == productId && c.UserId == userId)).FirstOrDefault();

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    _cartItemRepo.Update(cartItem);
                }
                else
                {
                    _cartItemRepo.Remove(cartItem);
                }

                await _cartItemRepo.SaveAsync();
            }

            return RedirectToAction("Index");
        }

    }
}
