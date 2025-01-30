namespace hw_20_dop_1;

public class OrderLine
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public Book? Book { get; set; }
}