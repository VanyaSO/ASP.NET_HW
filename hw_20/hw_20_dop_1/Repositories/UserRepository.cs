using System.Linq.Expressions;
using hw_20_dop_1.Data;
using hw_20_dop_1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hw_20_dop_1.Repositories;

public class UserRepository : IUser
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserWithCartByIdAsync(string id) => await _context.Users
        .Include(u => u.Cart)
        .ThenInclude(c => c.CartLines)
        .FirstOrDefaultAsync(u => u.Id.ToString() == id);
}