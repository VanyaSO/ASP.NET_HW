using hw_20_dop_1.Data;
using hw_20_dop_1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hw_20_dop_1.Repositories;

public class OrderRepository : IOrder
{
    private readonly ApplicationContext _context;

    public OrderRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string id) =>
        await _context.Orders
            .Include(o => o.OrderLines)
            .ThenInclude(ol => ol.Book)
            .Where(o => o.UserId.ToString() == id)
            .ToListAsync();

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }
}