using Microsoft.EntityFrameworkCore;

namespace hw_5.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
    {
    }
    
    public DbSet<Product> Products {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasIndex(e => e.Name);
    }
}