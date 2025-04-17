using Microsoft.EntityFrameworkCore;
using ECommerce.Data;
using ECommerce.Models;

namespace CartS.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext
        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get the cart for a specific customer
        public async Task<Cart> GetCartAsync(int customerId)
        {
            // Fetch the cart from the database where the customer ID matches
            var cart = await _context.Carts
                .Include(c => c.Items) // Include cart items if necessary
                .FirstOrDefaultAsync(c => c.CustomerId == customerId); // Assumes Cart has CustomerId

            return cart; // Returns the cart or null if no cart found
        }
    }

    // Interface for the CartService
    public interface ICartService
    {
        Task<Cart> GetCartAsync(int customerId);
    }
}
