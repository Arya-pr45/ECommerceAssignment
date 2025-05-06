using ECommerce.Models;
using ECommerce.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Interfaces
{
    public interface IOrderService
    {
        Task<OrderViewModel> GetOrdersForUserAsync(string userId);
        Task<bool> CreateOrderAsync(string userId, Address address);
    }
}
