using hw_8.Models;
using hw_8.ViewModels;

namespace hw_8.Services;

public class BookService
{
    List<Book> _books = new List<Book>
    {
        new Book { Id = 1, Title = "War and Peace", Author = "Leo Tolstoy", Genre = "Historical", Year = "1869" },
        new Book { Id = 2, Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Year = "1813" },
        new Book { Id = 3, Title = "1984", Author = "George Orwell", Genre = "Dystopian", Year = "1949" },
        new Book { Id = 4, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Classic", Year = "1925" },
        new Book { Id = 5, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Classic", Year = "1960" }
    };

    public List<BookViewModel> GetAllBooks() => _books
        .Select(b => new BookViewModel() { Title = b.Title, Author = b.Author, Genre = b.Genre, Year = b.Year })
        .ToList();

    public void Add(BookViewModel book)
    {
        int id = _books.Any() ? _books.Max(e => e.Id) + 1 : 1;
        _books.Add(new Book()
        {
            Id = id,
            Title = book.Title,
            Author = book.Author, 
            Genre = book.Genre,
            Year = book.Year
        });
    }
}