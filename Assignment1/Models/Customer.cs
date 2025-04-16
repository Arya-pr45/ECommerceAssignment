using System;

namespace Assignment1.Models
{
    public class Customer: User
    {
        public Customer(int userId,string userName): base(userId,userName, "Customer") { }

        public override void DisplayUserInfo()
        {
            Console.WriteLine($"Customer ID:{UserId}, Username:{UserName},Role: {Role}");
        }
        public void PlaceOrder()
        {
            Console.WriteLine($"Customer: {UserName} has placed an order");
        }
    }
}
