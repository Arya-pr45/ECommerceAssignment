using ECommerce.Models;
using ECommerce.Exceptions;
using ECommerce.Utilities;


namespace ECommerce.Services
{
    public delegate void OrderProcessedEventHandler(Order order);

    public class OrderService
    {
        private readonly List<Product> _inventory = new();
        private readonly Dictionary<int, Order> _orders = new();
        public event OrderProcessedEventHandler OrderProcessed;

        public void AddToInventory(Product product)
        {
            _inventory.Add(product);
            Logger.LogInfo($"Product added: {product.Name}");
        }

        public void CreateOrder(Order order)
        {
            _orders[order.OrderId] = order;
            Logger.LogInfo($"Order created: {order.OrderId}");
        }

        public async Task PlaceOrderAsync(int orderId)
        {
            if (!_orders.TryGetValue(orderId, out var order))
            {
                Logger.LogError($"Order not found: {orderId}");
                throw new OrderNotFoundException(orderId);
            }

            await Task.Delay(2000); // Simulate processing
            order.Status = OrderStatus.Processed;
            Logger.LogInfo($"Order processed: {orderId}");
            OnOrderProcessed(order);
        }

        public async Task<OrderStatus> GetOrderStatusAsync(int orderId)
        {
            return await Task.Run(() =>
            {
                if (_orders.TryGetValue(orderId, out var order))
                    return order.Status;
                return OrderStatus.Canceled;
            });
        }

        protected virtual void OnOrderProcessed(Order order)
        {
            OrderProcessed?.Invoke(order);
            Logger.LogInfo($"OrderProcessed event triggered for Order ID: {order.OrderId}");
        }
    }
}
