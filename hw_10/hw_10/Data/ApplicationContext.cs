using hw_10.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
    {
        Database.EnsureCreated();

        if (!Reviews.Any())
        {
            var authors = new List<Author>
            {
                new Author { Name = "F. Scott Fitzgerald" },
                new Author { Name = "George Orwell" }
            };
    
            var genres = new List<Genre>
            {
                new Genre { Name = "Classics" },
                new Genre { Name = "Dystopian" }
            };
            
            Authors.AddRange(authors);
            Genres.AddRange(genres);
            SaveChanges();
    
            Books.AddRange(new List<Book>
            {
                new Book
                {
                    Title = "The Great Gatsby",
                    AuthorId = 1,
                    GenreId = 1,
                    Year = "1925",
                    Description = "A novel about the American dream and the disillusionment of the Jazz Age.",
                    Price = 15.99m,
                    Image = "/images/sklad.jpg",
                    Reviews = new List<Review>
                    {
                        new Review { Text = "A masterpiece!", Rating = 5, UserName = "Ivan" },
                        new Review { Text = "Timeless and captivating.", Rating = 5, UserName = "Ivan" }
                    }
                },
                new Book
                {
                    Title = "1984",
                    AuthorId = 2,
                    GenreId = 2,
                    Year = "1949",
                    Description = "A chilling dystopia that explores the dangers of totalitarianism.",
                    Price = 12.99m,
                    Reviews = new List<Review>
                    {
                        new Review { Text = "Profoundly relevant.", Rating = 5, UserName = "Ivan" },
                        new Review { Text = "A cautionary tale for the ages.", Rating = 4, UserName = "Ivan" }
                    }
                }
            });
            SaveChanges();
        }
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>()
            .HasMany(b => b.Reviews)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId);
    }
}
