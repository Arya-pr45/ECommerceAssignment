using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Services.Interfaces;

namespace OrderS.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new();

        public async Task PlaceOrderAsync(Order order)
        {
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

        // ✅ Interface method implementations below:

        public List<Order> GetAllOrders()
        {
            return _orders;
        }

        public Order GetOrderById(int orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public void UpdateOrder(Order updatedOrder)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == updatedOrder.OrderId);
            if (existingOrder != null)
            {
                existingOrder.Status = updatedOrder.Status;
                existingOrder.OrderDate = updatedOrder.OrderDate;
                existingOrder.CustomerId = updatedOrder.CustomerId;
            }
        }
    }
}
