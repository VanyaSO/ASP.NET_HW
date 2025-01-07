using hw_14.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hw_14;

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
    
    public void UpdateUser(User user)
    {
        _db.Users.Update(user);
        _db.SaveChanges();
    }
}