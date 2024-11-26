using hw_6.Models;

namespace hw_6.Interfaces;

public interface IUser
{
    public void Register(User user);
    public string? LogIn(string email, string password);
    public bool ValidateToken(string token);
    public User? GetUserByToken(string token);
    public List<Course> GetCoursesForUser(string token);
}