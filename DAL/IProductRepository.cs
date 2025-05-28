using AegisTest.Models;

namespace AegisTest.DAL
{
    public interface IProductRepository : IDisposable
    {

        IEnumerable<Product> GetAllProductWithoutDescription();
    }
}