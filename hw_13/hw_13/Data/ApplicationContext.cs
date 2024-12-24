using hw_13.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
}