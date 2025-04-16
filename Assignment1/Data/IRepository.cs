using System.Collections.Generic;
using Assignment1.Models;

namespace Assignment1.Data
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Remove(int id);
        T Find(int id);
        IEnumerable<T> GetAll();
        IEnumerable<Product> GetProductsByCategory(string category);
        string? GetOrdersSortedByDate();
    }
}
