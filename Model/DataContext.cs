using Microsoft.EntityFrameworkCore;
using MODEL.Entities;

namespace MODEL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Products> Product { get; set; }
        public DbSet<SaleReports> SaleReport { get; set; }
    }
}
