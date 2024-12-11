using hw_10.Models;
using hw_10.Services;
using hw_10.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace hw_10.Controllers;

public class BookController : Controller
{
    private readonly BookService _books;
    public BookController(BookService books)
    {
        _books = books;
    }
        
    public async Task<IActionResult> Index()
    {
        IEnumerable<Book> books = await _books.GetAllBooksAsync();
        List<BookCardViewModel> bcvms = books.Select(b => new BookCardViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author.Name,
                Description = b.Description,
                Price = b.Price,
                Image = b.Image,
            }).ToList();
        return View(bcvms);
    }
    
    public async Task<IActionResult> Book(int bookId)
    {
        Book? book = await _books.GetBookByIdAsync(bookId);
        if (book is null) return NotFound("Book not found");
        
        BookPageViewModel bpvm = new BookPageViewModel
        {
            Title = book.Title,
            Author = book.Author.Name,
            Genre = book.Genre.Name,
            Year = book.Year,
            Description = book.Description,
            Price = book.Price,
            Image = book.Image, 
            Reviews = book.Reviews.Reverse().ToList()
        };

        return View(bpvm);
    }
}