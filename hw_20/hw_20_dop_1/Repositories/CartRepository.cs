using hw_20_dop_1.Data;
using hw_20_dop_1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hw_20_dop_1.Repositories;

public class CartRepository : ICart
{
    private readonly ApplicationContext _context;

    public CartRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetCartByIdAsync(string id) =>
        await _context.Carts
            .Include(c => c.CartLines)
            .ThenInclude(cl => cl.Book)
            .FirstOrDefaultAsync(c => c.Id.ToString() == id);
    
    public async Task UpdateCartAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }
    
}