using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.Products
{
    public interface IProduct
    {
        [Key]
        int ProductId { get; set; }
        [Required]
        [DisplayName("ProductName")]
        string Name { get; set; }
        [Required]
        decimal Price { get; set; }
        string Category { get; set; }
        string Description { get; set; }
        int Stock { get; set; }
        //string ImageUrl { get; set; }

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
        //public string ImageUrl { get; set; }

        public void DisplayProductInfo()
        {
            Console.WriteLine($"ID: {ProductId}, Name: {Name}, Price: ₹{Price}, Category:{Category}");
        }
    }
    public class ProductManage: Product
    {
        public char Action { get; set; }
    }
}