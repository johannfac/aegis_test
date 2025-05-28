using AegisTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AegisTest.Data
{
    public class AegisTestContext : DbContext
    {
        public AegisTestContext(DbContextOptions<AegisTestContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<ProductDescription> ProductDescription { get; set; } = default!;
    }
}
