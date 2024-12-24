using hw_13.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hw_13;

public class UserService
{
    private ApplicationContext _db;

    public UserService(ApplicationContext db)
    {
        _db = db;
    }

    public bool IsUsernameRegister(string username) => _db.Users.Any(u => u.Username.Equals(username));
    public bool IsEmailRegister(string email) => _db.Users.Any(u => u.Email.Equals(email));

    public User? GetUserByEmail(string email) => _db.Users.FirstOrDefault(u => u.Email.Equals(email));

    public void AddUser(User user)
    {
        _db.Users.Add(user);
        _db.SaveChanges();
    }
}