using AegisTest.Data;
using Microsoft.EntityFrameworkCore;

namespace AegisTest.Models;

public class ProductDescription
{
    public int Id { get; set; }
    public string? Description { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }
}

public static class SeedProductDescriptionData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (
            var context = new AegisTestContext(
                serviceProvider.GetRequiredService<DbContextOptions<AegisTestContext>>()
            )
        )
        {
            if (context.ProductDescription.Any())
            {
                return;
            }

            var products = context.Product.ToList();
            if (products.Count == 0)
            {
                throw new InvalidOperationException("No products found. Seed Product data first.");
            }

            context.ProductDescription.AddRange(
                new ProductDescription
                {
                    Description = "Description 1",
                    ProductId = products[0].Id
                },
                new ProductDescription
                {
                    Description = "Description 2",
                    ProductId = products[1].Id
                },
                new ProductDescription
                {
                    Description = "Description 3",
                    ProductId = products[2].Id
                },
                new ProductDescription
                {
                    Description = "Description 4",
                    ProductId = products[3].Id
                },
                new ProductDescription
                {
                    Description = "Description 5",
                    ProductId = products[4].Id
                }
            );
            context.SaveChanges();
        }
    }
}