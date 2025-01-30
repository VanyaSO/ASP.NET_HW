namespace hw_20_dop_1;

public class CartDTO
{
    public Guid Id { get; set; }
    public IEnumerable<CartLineDTO> CartLines { get; set; }
    public decimal TotalSum { get; set; }
}