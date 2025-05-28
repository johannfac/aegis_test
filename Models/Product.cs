using AegisTest.Data;
using Microsoft.EntityFrameworkCore;

namespace AegisTest.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public static class SeedProductData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (
            var context = new AegisTestContext(
                serviceProvider.GetRequiredService<DbContextOptions<AegisTestContext>>()
            )
        )
        {
            if (context.Product.Any())
            {
                return;
            }
            context.Product.AddRange(
                new Product
                {
                    Name = "Product 1"
                },
                new Product
                {
                    Name = "Product 2"
                },
                new Product
                {
                    Name = "Product 3"
                },
                new Product
                {
                    Name = "Product 4"
                },
                new Product
                {
                    Name = "Product 5"
                }
            );
            context.SaveChanges();
        }
    }
}