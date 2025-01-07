using hw_14.Models;
using Microsoft.AspNetCore.Identity;

namespace hw_14.Heplers;

public static class PasswordHelper
{
    private static readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
    
    public static string HashPassword(User user) => _passwordHasher.HashPassword(user, user.Password);
    
    public static bool VerifyPassword(User user, string enteredPassword)
    {
        try
        {
            Convert.FromBase64String(user.Password);
        }
        catch (FormatException)
        {
            return false;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, enteredPassword);
        return result == PasswordVerificationResult.Success;
    }
}