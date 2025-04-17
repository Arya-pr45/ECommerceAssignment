namespace ECommerce.Models
{
    public static class UserFactory
    {
        public static User CreateUser(string role, int userId, string userName, string password,
                               string email = "", string phoneNumber = "")
        {
            return role switch
            {
                //"Admin" => new Admin(userId, userName, password),
                //"Customer" => new Customer(userId, userName, password, email, phoneNumber),
                _ => throw new ArgumentException("Invalid user role")
            };
        }
    };
        }
  