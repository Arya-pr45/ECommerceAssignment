using System;
using ECommerce.Models;

namespace Ecommerce.Models
{
    public class Customer: User
    {
        public Customer(int userId,string userName,string password): base(userId,userName,password, "Customer") { }

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
