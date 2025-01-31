namespace hw_21.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public DateTime? DeleteAt { get; set; }
}