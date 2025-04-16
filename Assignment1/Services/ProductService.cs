using System.Collections.Generic;
using System.Linq;
using Assignment1.Models;
using Assignment1.Services.Interfaces;

namespace ECommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new();

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.ProductId == id);
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void RemoveProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
                _products.Remove(product);
        }
    }
}
