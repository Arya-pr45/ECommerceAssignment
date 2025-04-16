// Services/Interfaces/IUserService.cs
using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        bool Register(string username, string password, string role);
        User GetUserByUsername(string username);
        public List<User> GetAllUsers();
        

    }
}
