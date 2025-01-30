namespace hw_20_dop_1;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public OrderData OrderData { get; set; }
    public bool Shipped { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; }
    public User? User { get; set; }
}