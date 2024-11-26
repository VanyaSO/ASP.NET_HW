using hw_6.Interfaces;
using hw_6.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace hw_6.Repositories;

public class UserRepository : IUser
{
    private readonly ApplicationContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    
    public UserRepository(ApplicationContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public void Register(User user)
    {
        if (_context.Users.Any(u => u.Email == user.Email)) return;

        user.Token = Guid.NewGuid().ToString();
        user.Password = HashPassword(user, user.Password);
        
        _context.Users.Add(user); 
        _context.SaveChanges();
    }

    public string? LogIn(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user != null && !VerifyPassword(user, user.Password, password)) return user.Token;
        
        return null;
    }
    
    public bool ValidateToken(string token) => token.IsNullOrEmpty() || _context.Users.Any(u => u.Token == token);

    public User? GetUserByToken(string token) => _context.Users.FirstOrDefault(u => u.Token == token);

    public List<Course> GetCoursesForUser(string token)
    {
        var user = _context.Users.Include(u => u.Courses).FirstOrDefault(u => u.Token == token);
        return user?.Courses ?? new List<Course>();
    }
    
    private string HashPassword(User user, string password) => _passwordHasher.HashPassword(user, password);
    
    private bool VerifyPassword(User user, string enteredPassword, string storedHash)
    {
        try
        {
            Convert.FromBase64String(storedHash);
        }
        catch (FormatException)
        {
            return false;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, storedHash, enteredPassword);
        return result == PasswordVerificationResult.Success;
    }

}