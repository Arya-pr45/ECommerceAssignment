using ECommerce.Models;

namespace ECommerce.Data
{
    public class Repository<T> : IRepository<T>
    {
        private readonly List<T> _entities = new();

        public void Add(T entity)
        {
            _entities.Add(entity);

        }

        public void Remove(int id)
        {
            var entity = _entities.FirstOrDefault(e => e.GetHashCode() == id);
            if (entity != null)
                _entities.Remove(entity);
        }

        public T Find(int id)
        {
            return _entities.FirstOrDefault(e => e.GetHashCode() == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities;
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _entities.OfType<Product>().Where(p => p.Category == category);
        }

        public IEnumerable<Order> GetOrdersSortedByDate()
        {
            return _entities.OfType<Order>().OrderBy(o => o.OrderDate);
        }

    }
}
