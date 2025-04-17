using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Services.Interfaces;

namespace ProductS.Services
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

        public void UpdateProduct(Product updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Stock = updatedProduct.Stock;
            }
        }
    }
}