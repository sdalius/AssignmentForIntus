using Microsoft.EntityFrameworkCore;

namespace TestForDotNet.Repository.Models.Context
{
    public class OrdersDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<SubElement> SubElements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}