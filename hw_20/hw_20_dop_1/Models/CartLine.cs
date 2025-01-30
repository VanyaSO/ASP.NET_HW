namespace hw_20_dop_1;

public class CartLine
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public int Quantity { get; set; }
    public Guid BookId { get; set; }
    public Book? Book { get; set; }
}