using Ecommerce.Models;
using ECommerce.Models;
using ECommerce.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UserS.Services
{
    public class UserService : IUserService
    {
        private static List<User> _users = new List<User>();
        private static int _idCounter = 1;

        public User Authenticate(string username, string password)
        {
            return _users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }

        public bool Register(string username, string password, string role)
        {
            if (_users.Any(u => u.UserName == username))
                return false;

            User newUser;
            int newUserId = _idCounter++;

            if (role.ToLower() == "admin")
                newUser = new Admin(newUserId, username, password);
            else
                newUser = new Customer(newUserId, username, password);

            newUser.Role = role;
            _users.Add(newUser);
            return true;
        }

        public User GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.UserName == username);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }
    }
}
