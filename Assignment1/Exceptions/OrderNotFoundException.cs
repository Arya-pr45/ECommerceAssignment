namespace ECommerce.Exceptions
{
    public class OrderNotFoundException: Exception
    {
        public OrderNotFoundException(int orderId):base($"Order with ID {orderId} was not found.") { }
    }
}
