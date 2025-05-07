using ECommerce.Interfaces;
using ECommerce.Models.Products;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services
{
    public class ProductS : Repository<Product>, IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductS(ApplicationDbContext context) : base(context) { 
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> FindByCategoryAsync(string category)
        {
            if (string.IsNullOrEmpty(category))
                return Enumerable.Empty<Product>();

            var filtered = await _context.Products
                .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return filtered;
        }
    }
}
