using System;

namespace Assignment1.Models
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public User(int userId, string userName,string role)
        {
            UserId = userId;
            UserName = userName;
            Role = role;
        }

        public abstract void DisplayUserInfo();
    }
}
