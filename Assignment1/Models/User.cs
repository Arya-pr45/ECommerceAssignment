using System;

namespace ECommerce.Models
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }


        public User(int userId, string userName,string role, string password)
        {
            UserId = userId;
            UserName = userName;
            Role = role;
            Password = password;
        }

        public abstract void DisplayUserInfo();
    }
}
