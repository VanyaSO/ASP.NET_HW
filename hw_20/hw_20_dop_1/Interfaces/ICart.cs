namespace hw_20_dop_1.Interfaces;

public interface ICart
{
    public Task<Cart?> GetCartByIdAsync(string id);
    public Task UpdateCartAsync(Cart cart);
}