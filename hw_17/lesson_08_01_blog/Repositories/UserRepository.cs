using lesson_08_01_blog.Interfaces;
using lesson_08_01_blog.Models;
using Microsoft.EntityFrameworkCore;

namespace lesson_08_01_blog.Repositories;

public class UserRepository : IUser
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<User> GetUserWithPublicationsByUserNameAsync(string username)
    {
        return await _context.Users.Include(u => u.Publications).ThenInclude(p => p.Categories).FirstOrDefaultAsync(u => u.UserName.Equals(username));
    }
}