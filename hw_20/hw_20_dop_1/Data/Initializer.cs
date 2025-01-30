using hw_20_dop_1;
using hw_20_dop_1.Data;
using Microsoft.EntityFrameworkCore;

public static class Initializer
{
    public static void Init(ApplicationContext context)
    {
        // context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        List<Author> authors = new List<Author>()
        {
            new Author { Name = "Jon Skeet" },
            new Author { Name = "Robert C. Martin" },
            new Author { Name = "Andrew Hunt" }
        };
        if (!context.Authors.Any())
        {
            context.Authors.AddRange(authors);
            context.SaveChanges();
        }

        List<Genre> genres = new List<Genre>()
        {
            new Genre { Name = "Programming" },
            new Genre { Name = "Software Engineering" }
        };
        if (!context.Genres.Any())
        {
            context.Genres.AddRange(genres);
            context.SaveChanges();
        }

        List<Book> books = new List<Book>()
        {
            new Book
            {
                Title = "C# in Depth",
                Description = "Advanced C# programming book",
                Price = 39.99M,
                AuthorId = authors[0].Id,
                GenreId = genres[0].Id
            },
            new Book
            {
                Title = "Clean Code",
                Description = "Software engineering principles",
                Price = 49.99M,
                AuthorId = authors[1].Id,
                GenreId = genres[1].Id
            }
        };
        if (!context.Books.Any())
        {
            context.Books.AddRange(books);
            context.SaveChanges();
        }

        var user = new User
        {
            FullName = "John Doe",
            Email = "user1@example.com",
            Password = "password1",
            Cart = new Cart
            {
                LastUpdate = DateTime.Now,
                CartLines = new List<CartLine>
                {
                    new CartLine { Quantity = 2, BookId = books[0].Id },
                    new CartLine { Quantity = 1, BookId = books[1].Id }
                }
            },
            Orders = new List<Order>()
            {
                new Order
                {
                    Id = Guid.NewGuid(),
                    OrderData = new OrderData()
                    {
                        CustomerName = "Jane Smith",
                        City = "New York",
                        Address = "123 Main St, Apt 4B",
                    },
                    Shipped = false,
                    OrderLines = new List<OrderLine>
                    {
                        new OrderLine
                        {
                            BookId = books[0].Id,
                            Quantity = 1
                        },
                        new OrderLine
                        {
                            BookId = books[1].Id,
                            Quantity = 2
                        }
                    }
                },
                new Order
                {
                    Id = Guid.NewGuid(),
                    OrderData = new OrderData()
                    {
                        CustomerName = "Mark Johnson",
                        City = "Los Angeles",
                        Address = "456 Elm St",   
                    },
                    Shipped = true,
                    OrderLines = new List<OrderLine>
                    {
                        new OrderLine
                        {
                            BookId = books[1].Id,
                            Quantity = 1
                        }
                    }
                }
            }
        };
        if (!context.Users.Any())
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}