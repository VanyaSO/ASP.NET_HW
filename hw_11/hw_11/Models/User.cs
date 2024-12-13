namespace hw_11.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Password { get; set; }
    public string? CreditCardNumber { get; set; }
    public string? WebSite { get; set; }
    public bool TermsOfService { get; set; }
}