using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void RemoveProduct(int id);
        void UpdateProduct(Product product);
    }
}
