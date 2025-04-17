using ECommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(int customerId);
        Task AddToCartAsync(int customerId, int productId, int quantity);
        Task RemoveFromCartAsync(int customerId, int productId);
        Task ClearCartAsync(int customerId);
    }
}
