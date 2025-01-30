namespace hw_20_dop_1.Interfaces;

public interface IUser
{
    public Task<User?> GetUserWithCartByIdAsync(string id);
}