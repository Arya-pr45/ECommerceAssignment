using System;

namespace Assignment1.Models
{
    public interface IProduct
    {
        int ProductId { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        string Category { get; set; }
        void DisplayProductInfo();
    }

    public class Product : IProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public void DisplayProductInfo()
        {
            Console.WriteLine($"ID: {ProductId}, Name: {Name}, Price: ₹{Price}, Category:{Category}");
        }
    }
}