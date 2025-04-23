using System.Linq.Expressions;

namespace ECommerce.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        T Find(int id);
        void Update(T entity);
        void Remove(T entity);

        Task SaveAsync();
    }
}
