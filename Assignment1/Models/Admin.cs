using System;

namespace Assignment1.Models
{
    public class Admin:User
    {
        public Admin(int userId, string userName):base(userId, userName,"Admin") { }

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
