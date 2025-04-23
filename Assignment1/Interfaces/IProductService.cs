using ECommerce.Models.Products;

namespace ECommerce.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void RemoveProduct(int id);
        //bool IsInStock(int productId);
        void UpdateProduct(Product product);
    }
}
