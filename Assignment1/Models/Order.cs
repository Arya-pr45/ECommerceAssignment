
using Ecommerce.Models;

namespace ECommerce.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        //public List<OrderItem> Items { get; set; }
    }
    public enum OrderStatus
    {
        Pending,
        Processed,
        Shipped,
        Delivered,
        Canceled
    }
}
