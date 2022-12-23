using ETrade.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ETrade.API.Data
{
    public class DataContext:DbContext
    {
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<Cart> Carts { get; set; }
    }
}
