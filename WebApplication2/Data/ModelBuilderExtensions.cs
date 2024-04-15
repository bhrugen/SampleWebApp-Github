using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product 1", Price = 10.0m, Category = "Category 1", Color = "Red" },
                new Product { Id = 2, Name = "Product 2", Price = 20.0m, Category = "Category 2", Color = "Blue" },
                new Product { Id = 3, Name = "Product 3", Price = 30.0m, Category = "Category 3", Color = "Green" },
                new Product { Id = 4, Name = "Product 4", Price = 40.0m, Category = "Category 4", Color = "Yellow" },
                new Product { Id = 5, Name = "Product 5", Price = 50.0m, Category = "Category 5", Color = "Black" }
            );
        }
       
    }
}
