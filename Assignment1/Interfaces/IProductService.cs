using ECommerce.Models.Products;
using ECommerce.Repository;

namespace ECommerce.Interfaces
{
    public interface IProductService : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> FindByCategoryAsync(string category);
    }
}
