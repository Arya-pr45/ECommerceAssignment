using Ecommerce.Models;
using ECommerce.Models;

namespace ECommerce.Services
{
    public static class UserFactory
    {
        public static User? CreateUser(string role, int userId, string username,string password)
        {
            return role.ToLower() switch
            {
                "admin" => new Admin(userId, username,password),
                "customer" => new Customer(userId, username,password),
                _ => null
            };
        }
    }
}
