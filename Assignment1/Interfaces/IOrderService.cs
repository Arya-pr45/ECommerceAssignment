using ECommerce.Models;
using ECommerce.Models.Orders;

namespace ECommerce.Interfaces
{
    public interface IOrderService
    {
        Task<OrderViewModel> GetOrdersForUserAsync(string userId);
        Task<bool> CreateOrderAsync(string userId, Address address);
    }
}
