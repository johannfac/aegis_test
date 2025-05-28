using AegisTest.Data;
using AegisTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AegisTest.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly AegisTestContext _context;

        public ProductRepository(AegisTestContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProductWithoutDescription()
        {
            return _context.Product
                .FromSqlRaw("SELECT * FROM GetAllProductWithDescription()")
                .ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
