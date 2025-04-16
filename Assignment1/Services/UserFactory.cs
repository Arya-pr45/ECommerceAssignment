using Assignment1.Models;
using Assignment1.Models;

namespace ECommerce.Services
{
    public static class UserFactory
    {
        public static User CreateUser(string role, int id, string username)
        {
            return role.ToLower() switch
            {
                "admin" => new Admin { UserId = id, Username = username, Role = "Admin" },
                "customer" => new Customer { UserId = id, Username = username, Role = "Customer" },
                _ => null
            };
        }
    }
}