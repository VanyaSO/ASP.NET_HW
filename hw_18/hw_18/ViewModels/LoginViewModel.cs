using System.ComponentModel.DataAnnotations;

namespace hw_18.ViewModels;

public class LoginViewModel
{
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}