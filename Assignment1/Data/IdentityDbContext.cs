using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data
{
    public class IdentityDbContext<T>
    {
        private DbContextOptions options;

        public IdentityDbContext(DbContextOptions options)
        {
            this.options = options;
        }

        internal void OnModelCreating(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }
    }
}