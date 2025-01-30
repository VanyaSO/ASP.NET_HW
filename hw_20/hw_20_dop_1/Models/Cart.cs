namespace hw_20_dop_1;

public class Cart
{
    public Guid Id { get; set; }
    public DateTime LastUpdate { get; set; }
    public User? User { get; set; }
    public ICollection<CartLine> CartLines { get; set; }
}