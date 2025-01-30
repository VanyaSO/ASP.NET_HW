namespace hw_20_dop_1;

public class OrderDTO
{
    public Guid Id { get; set; }
    public OrderData OrderData { get; set; }
    public bool Shipped { get; set; }
    public IEnumerable<OrderLineDTO> OrderLines { get; set; }
    public decimal TotalSum { get; set; }
}