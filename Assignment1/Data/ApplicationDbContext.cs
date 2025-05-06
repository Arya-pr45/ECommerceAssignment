using ECommerce.Models;
using ECommerce.Models.Carts;
using ECommerce.Models.Orders;
using ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }        
        public DbSet<Order> Orders { get; set; }        
        public DbSet<OrderItem> OrderItems { get; set; }        
        public DbSet<Address> Addresses { get; set; }        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

  
        }
    }
}
