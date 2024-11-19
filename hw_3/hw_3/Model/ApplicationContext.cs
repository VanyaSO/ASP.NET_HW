using Microsoft.EntityFrameworkCore;
namespace hw_3.Model;

public class ApplicationContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { }
}