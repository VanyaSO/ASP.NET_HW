using hw_9.Models;
using hw_9.Services;
using hw_9.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hw_9.Controllers;

public class BookController : Controller
{
    private readonly BookService _books;
    private readonly GenreService _genres;
    public BookController(BookService books, GenreService genres)
    {
        _books = books;
        _genres = genres;
    }
        
    public IActionResult Index()
    {
        List<BookViewModel> books = _books.GetAllBooks().Select(b => new BookViewModel()
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            Genre = _genres.GetGenreById(b.GenreId)?.Name ?? "-",
            Year = b.Year,
            Description = b.Description
        }).ToList();
        return View(new TableViewModel(books));
    }
    
    public IActionResult Update(int id)
    {
        Book? book = _books.GetBookById(id);
        if (book is null) return NotFound("Product not found");
        
        ViewBag.BookId = id;
        ViewBag.Genres = new SelectList(_genres.GetAllGenres(), "Id", "Name");
        return View(book);
    }
    
    [HttpPost]
    public IActionResult Update(Book book)
    {
        _books.UpdateBook(book);
        return RedirectToAction(nameof(Index));
    }
}