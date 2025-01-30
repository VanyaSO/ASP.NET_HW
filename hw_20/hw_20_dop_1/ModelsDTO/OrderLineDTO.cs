namespace hw_20_dop_1;

public class OrderLineDTO
{
    public int Quantity { get; set; }
    public OrderBookDTO Book { get; set; }
    public decimal TotalSum { get; set; }
}