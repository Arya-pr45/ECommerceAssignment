using ECommerce.Models;
using System.Collections.Generic;

namespace ECommerce.Services.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int orderId);
        void UpdateOrder(Order order);
    }
}
