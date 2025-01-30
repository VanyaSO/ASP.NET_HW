namespace hw_20_dop_1;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid CartId { get; set; }
    public Cart? Cart { get; set; }
    public ICollection<Order> Orders { get; set; }
}