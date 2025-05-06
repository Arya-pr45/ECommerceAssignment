using ECommerce.Models.Carts;
using ECommerce.Models.Products;
using ECommerce.Repository;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services
{
    public class CartS : ICartService
    {
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<Product> _productRepo;

        public CartS(IRepository<Cart> cartRepo, IRepository<Product> productRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        public async Task<CartViewModel> GetCartViewModelAsync(string userId)
        {
            var cartItems = await _cartRepo.FindAsync(c => c.UserId == userId);
            var cartItemList = new List<CartItem>();

            foreach (var item in cartItems)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                cartItemList.Add(new CartItem
                {
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                });
            }

            return new CartViewModel { cartList = cartItemList };
        }

        public async Task AddToCartAsync(int productId, string userId)
        {
            var product = _productRepo.Find(productId);
            if (product == null || product.Stock <= 0) return;

            var existingItem = (await _cartRepo.FindAsync(c => c.ProductId == productId && c.UserId == userId)).FirstOrDefault();

            if (existingItem != null)
            {
                if (existingItem.Quantity < product.Stock)
                {
                    existingItem.Quantity++;
                    _cartRepo.Update(existingItem);
                }
                else
                {
                }
            }
            else
            {
                await _cartRepo.AddAsync(new Cart
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = 1,
                    Product = product
                });
            }

            await _cartRepo.SaveAsync();
        }


        public async Task RemoveFromCartAsync(int productId, string userId)
        {
            var item = (await _cartRepo.FindAsync(c => c.ProductId == productId && c.UserId == userId)).FirstOrDefault();
            if (item != null)
            {
                _cartRepo.Remove(item);
                await _cartRepo.SaveAsync();
            }
        }

        public async Task IncreaseQuantityAsync(int productId, string userId)
        {
            var item = (await _cartRepo.FindAsync(c => c.ProductId == productId && c.UserId == userId)).FirstOrDefault();
            var product = await _productRepo.GetByIdAsync(productId);

            if (item != null && product != null && item.Quantity < product.Stock)
            {
                item.Quantity++;
                _cartRepo.Update(item);
                await _cartRepo.SaveAsync();
            }
        }


        public async Task DecreaseQuantityAsync(int productId, string userId)
        {
            var item = (await _cartRepo.FindAsync(c => c.ProductId == productId && c.UserId == userId)).FirstOrDefault();
            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                    _cartRepo.Update(item);
                }
                else
                {
                    _cartRepo.Remove(item);
                }

                await _cartRepo.SaveAsync();
            }
        }
    }
}
