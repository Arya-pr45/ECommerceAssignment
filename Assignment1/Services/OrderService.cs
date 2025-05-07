using ECommerce.Interfaces;
using ECommerce.Models;
using ECommerce.Models.Carts;
using ECommerce.Models.Orders;
using ECommerce.Models.Products;
using ECommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services
{
    public class OrderS : IOrderService
    {
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<OrderItem> _orderItemRepo;
        private readonly IAddressService _addressService;

        public OrderS(
            IRepository<Cart> cartRepo,
            IRepository<Product> productRepo,
            IRepository<Order> orderRepo,
            IRepository<OrderItem> orderItemRepo,
            IAddressService addressService)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _addressService = addressService;
        }

        public async Task<OrderViewModel> GetOrdersForUserAsync(string userId)
        {
            var orders = await _orderRepo.FindAsync(o => o.UserId == userId);
            var orderViewModel = new OrderViewModel();

            foreach (var order in orders)
            {
                var items = await _orderItemRepo.FindAsync(i => i.OrderId == order.OrderId);
                foreach (var item in items)
                {
                    var product = await _productRepo.GetByIdAsync(item.ProductId);
                    if (product == null) continue;

                    orderViewModel.orderList.Add(new OrderView
                    {
                        OrderDate = order.OrderDate,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = item.Quantity,
                        OrderId = order.OrderId,
                        Status = order.Status,
                        TotalAmount = order.TotalAmount
                    });
                }
            }

            return orderViewModel;
        }

        public async Task<bool> CreateOrderAsync(string userId, Address address)
        {
            var cartItems = await _cartRepo.FindAsync(c => c.UserId == userId);
            if (!cartItems.Any()) return false;

            decimal totalAmount = 0;

            foreach (var cartItem in cartItems)
            {
                var product = await _productRepo.GetByIdAsync(cartItem.ProductId);

                if (product == null || cartItem.Quantity > product.Stock)
                {
                    throw new Exception($"Product '{product?.Name}' is out of stock or has insufficient quantity.");
                }

                totalAmount += product.Price * cartItem.Quantity;
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                ShippingAddress = address, 
                TotalAmount = totalAmount,
                OrderItems = cartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Product?.Price ?? 0
                }).ToList()
            };

            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveAsync();

            foreach (var cartItem in cartItems)
            {
                var product = await _productRepo.GetByIdAsync(cartItem.ProductId);
                if (product != null)
                {
                    product.Stock -= cartItem.Quantity;
                    _productRepo.Update(product);
                }

                _cartRepo.Remove(cartItem);
            }

            await _productRepo.SaveAsync();
            await _cartRepo.SaveAsync();

            return true;
        }


    }
}
