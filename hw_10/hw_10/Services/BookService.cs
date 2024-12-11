using hw_10.Models;
using Microsoft.EntityFrameworkCore;

namespace hw_10.Services;

public class BookService
{
    private readonly ApplicationContext _context;
    
    public BookService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooksAsync() => await _context.Books
        .Include(b => b.Author)
        .Include(b => b.Genre)
        .Include(b => b.Reviews)
        .ToListAsync();

    public async Task<Book?> GetBookByIdAsync(int id) => await _context.Books
        .Include(b => b.Author)
        .Include(b => b.Genre)
        .Include(b => b.Reviews)
        .FirstOrDefaultAsync(b => b.Id == id);
}