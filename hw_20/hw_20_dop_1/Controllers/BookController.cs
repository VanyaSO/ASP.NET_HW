using Microsoft.AspNetCore.Mvc;
using hw_20_dop_1;
using hw_20_dop_1.Interfaces;


[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBook _books;

    public BookController(IBook books)
    {
        _books = books;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksAsync()
    {
        var books = await _books.GetBooksWithFullInfoAsync();
        return books.Select(b => BookDTO(b)).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDTO>> GetBookAsync(string id)
    {
        var book = await _books.GetBookWithFullInfoByIdAsync(id);
        if (book == null) return NotFound();

        return BookDTO(book);
    }

    [HttpGet("searchbook")]
    public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBookAsync(string? propertyName, string? searchTerm)
    {
        if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(searchTerm))
            return BadRequest("Property name and search term not be empty");

        var searchBooks = await _books.SearchBookAsync(propertyName, searchTerm);
        return searchBooks.Select(b => BookDTO(b)).ToList();
    }

    private static BookDTO BookDTO(Book book) => new BookDTO()
    {
        Id = book.Id,
        Title = book.Title,
        Description = book.Description,
        Price = book.Price,
        AuthorId = book.Author.Id,
        AuthorName = book.Author.Name,
        GenreId = book.Genre.Id,
        GenreName = book.Genre.Name
    };
}