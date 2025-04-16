using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Enums;
using Assignment1.Models;
using Assignment1.Services.Interfaces;
using Assignment1.Models;
using Assignment1.Services.Interfaces;

namespace ECommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new();

        public async Task PlaceOrderAsync(Order order)
        {
            // Simulate async order processing
            await Task.Delay(2000); 
            order.Status = OrderStatus.Processed;
            order.OrderDate = DateTime.Now;
            _orders.Add(order);
        }

        public async Task<OrderStatus> GetOrderStatusAsync(int orderId)
        {
            return await Task.Run(() =>
            {
                var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
                return order?.Status ?? OrderStatus.Canceled;
            });
        }
    }
}
