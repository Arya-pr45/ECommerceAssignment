
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    // Represents a shopping cart
    public class Cart
    {
        public int Id { get; set; }
        public int userId { get; set; }

        [ForeignKey("userId")]

        public bool IsCheckedOut { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

    }
}
