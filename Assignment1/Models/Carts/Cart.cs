using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Models.Products;

namespace ECommerce.Models.Carts
{
    public class Cart
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

    }
    public class CartViewModel
    {
        public CartViewModel()
        {
            cartList = new List<CartItem>();
        }
       public List<CartItem> cartList { get; set; } 
    }
}
