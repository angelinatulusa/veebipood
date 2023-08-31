using Microsoft.EntityFrameworkCore;
using veebipood.Models;

namespace veebipood.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
