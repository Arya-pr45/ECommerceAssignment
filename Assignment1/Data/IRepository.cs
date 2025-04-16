using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Data
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Remove(int id);
        T Find(int id);
        IEnumerable<T> GetAll();
        IEnumerable<Product> GetProductsByCategory(string category);
        IEnumerable<Order> GetOrdersSortedByDate();
    }
}
