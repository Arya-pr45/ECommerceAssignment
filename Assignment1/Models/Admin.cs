using System;

namespace ECommerce.Models
{
    public class Admin:User
    {
        public Admin(int userId, string userName,string password):base(userId, userName,password,"Admin") { }

        public override void DisplayUserInfo()
        {
            Console.WriteLine($"UserID:{UserId},Username:{UserName},Role:{Role}");
        }
        public void ManageInventory()
        {
            Console.WriteLine($"{UserName} is managing Inventory");
        }
    }
}
