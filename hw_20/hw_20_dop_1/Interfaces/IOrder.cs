namespace hw_20_dop_1.Interfaces;

public interface IOrder
{
    public Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string id);
    public Task AddOrderAsync(Order order);
}