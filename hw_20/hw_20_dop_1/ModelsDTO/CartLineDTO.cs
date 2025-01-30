namespace hw_20_dop_1;

public class CartLineDTO
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid BookId { get; set; }
    public string BookTitle { get; set; }
    public decimal BookPrice { get; set; }
    public decimal TotalSum { get; set; }
}