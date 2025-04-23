using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ECommerce.Models
{
    public class User  
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        //public string IsInRole { get; internal set; }

        public User() { }

        public void DisplayUserInfo()
        {
            
        }

        public enum UserRole
        {
            Admin,
            Customer
        }
    }
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
