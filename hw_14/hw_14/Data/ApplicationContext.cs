using hw_14.Heplers;
using hw_14.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Product> Products { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();

        if (!Currencies.Any())
        {
            Currencies.AddRange(new List<Currency>
            {
                new Currency { Code = "USD", Rate = 1.0m, Symbol = "$" },
                new Currency { Code = "EUR", Rate = 0.93m, Symbol = "€" },
                new Currency { Code = "UAH", Rate = 42m, Symbol = "₴" }
            });
        }
        
        if (!Users.Any())
        {
            User admin = new User() { Username = "Admin", Email = "admin@gmail.com", Password = "admin"};
            admin.Password = PasswordHelper.HashPassword(admin);
            
            Users.Add(admin);
        }
        
        if (!Products.Any())
        {
            Products.AddRange(new List<Product>
            {
                new Product { Name = "Laptop", Description = "High-performance laptop", Price = 1000.0m },
                new Product { Name = "Smartphone", Description = "Latest model smartphone", Price = 700.0m },
                new Product { Name = "Headphones", Description = "Noise-cancelling headphones", Price = 150.0m }
            });
        }

        SaveChanges();
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
}