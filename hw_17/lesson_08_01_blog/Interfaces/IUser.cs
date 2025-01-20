using lesson_08_01_blog.Models;

namespace lesson_08_01_blog.Interfaces;

public interface IUser
{
    public Task<User> GetUserWithPublicationsByUserNameAsync(string username);
}