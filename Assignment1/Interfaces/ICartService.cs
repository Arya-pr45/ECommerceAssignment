using ECommerce.Models.Carts;

namespace ECommerce.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartViewModel> GetCartViewModelAsync(string userId);
        Task AddToCartAsync(int productId, string userId);
        Task RemoveFromCartAsync(int productId, string userId);
        Task IncreaseQuantityAsync(int productId, string userId);
        Task DecreaseQuantityAsync(int productId, string userId);
    }
}
