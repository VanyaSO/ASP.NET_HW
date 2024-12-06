using hw_9.Models;
using hw_9.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace hw_9.Services;

public class BookService
{
    public readonly GenreService _genres;
    public BookService(GenreService genres)
    {
        _genres = genres;
    }
    
    List<Book> _books = new List<Book>
    {
        new Book { Id = 1, Title = "War and Peace", Author = "Leo Tolstoy", GenreId = 1, Year = "1869" },
        new Book { Id = 2, Title = "Pride and Prejudice", Author = "Jane Austen", GenreId = 2, Year = "1813" },
        new Book { Id = 3, Title = "1984", Author = "George Orwell", GenreId = 3, Year = "1949" },
        new Book { Id = 4, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", GenreId = 3, Year = "1925" },
        new Book { Id = 5, Title = "To Kill a Mockingbird", Author = "Harper Lee", GenreId = 1, Year = "1960" }
    };

    public List<Book> GetAllBooks() => _books.ToList();

    public Book? GetBookById(int id) => _books
        .FirstOrDefault(b => b.Id == id);

    public void UpdateBook(Book book)
    {
        int index = _books.FindIndex(b => b.Id == book.Id);
        _books[index] = book;
    }
}