using Microsoft.EntityFrameworkCore;

namespace hw_20_dop_1.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartLine> СartLines { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<Book>()
            .Property(b => b.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<User>(u => u.CartId);
        
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.CartLines)
            .WithOne()
            .HasForeignKey(cl => cl.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasMany(c => c.OrderLines)
            .WithOne()
            .HasForeignKey(cl => cl.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderLine>()
            .HasOne(ol => ol.Book)
            .WithMany()
            .HasForeignKey(ol => ol.BookId)
            .OnDelete(DeleteBehavior.NoAction);
    }

}