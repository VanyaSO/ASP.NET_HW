using System.Linq.Expressions;
using hw_20_dop_1.Data;
using hw_20_dop_1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hw_20_dop_1.Repositories;

public class BookRepository : IBook
{
    private readonly ApplicationContext _context;

    public BookRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Book>> GetBooksWithFullInfoAsync() => await _context.Books
        .Include(b => b.Author)
        .Include(b => b.Genre)
        .ToListAsync(); 
    
    public async Task<Book?> GetBookWithFullInfoByIdAsync(string id) => await _context.Books
        .Include(b => b.Author)
        .Include(b => b.Genre)
        .FirstOrDefaultAsync(b => b.Id.ToString() == id);

    public async Task<IEnumerable<Book>> SearchBookAsync(string propertyName, string searchTerm)
    {
        IQueryable<Book> query = Search(_context.Books.Include(b => b.Author).Include(b => b.Genre), propertyName, searchTerm);
        return await query.ToListAsync();
    }

    public async Task<bool> BookExistsAsync(string id) => await _context.Books.AnyAsync(b => b.Id.ToString() == id);
    
    private static IQueryable<T> Search<T>(IQueryable<T> query, string propertyName, string searchTerm)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var source = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
        var body = Expression.Call(source, "Contains", Type.EmptyTypes,
            Expression.Constant(searchTerm, typeof(string)));
        var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
        return query.Where(lambda);
    }
}