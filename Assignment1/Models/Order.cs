using Assignment1.Models.Enums;

namespace Assignment1.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
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
