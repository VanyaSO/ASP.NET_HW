using Microsoft.EntityFrameworkCore;

namespace hw_5.Models;

public class Database
{
    private readonly ApplicationContext _context;
    
    public Database(ApplicationContext context)
    {
        _context = context;
    }
    
    public void Init()
    {
        _context.Database.EnsureDeleted();
        _context.Database.Migrate();
        
        if (!_context.Products.Any())
        {
            var products = new[]
            {
                new Product { Name = "Smartphone Galaxy X10", Description = "6.7 inch AMOLED display", Price = 599.99 },
                new Product { Name = "Laptop ProBook 15", Description = "Intel Core i7 processor", Price = 1099.99 },
                new Product { Name = "Wireless Headphones SoundFree X", Description = "Active noise cancellation", Price = 129.99 }
            };

            _context.Products.AddRange(products);
            _context.SaveChanges();
        }
    }
}