using hw_4.Model;

namespace hw_4.Interfaces;

public interface IUser
{
    public Task<IEnumerable<User>> GetAllUsers();
    public Task DeleteUserById(int id);
    public Task UpdateUser(User user);
    public Task<IEnumerable<User>> SearchUsers(string option, string value);
}