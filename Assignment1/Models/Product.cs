using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public interface IProduct
    {
        [Key]
        int ProductId { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        string Category { get; set; }
        string Description {  get; set; }
        int Stock { get; set; }
        void DisplayProductInfo();
    }

    public class Product : IProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        public void DisplayProductInfo()
        {
            Console.WriteLine($"ID: {ProductId}, Name: {Name}, Price: ₹{Price}, Category:{Category}");
        }
    }
}