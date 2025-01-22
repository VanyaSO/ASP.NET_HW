using System.ComponentModel.DataAnnotations;

namespace hw_18.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? FirstName { get; set; }
    
    [Required]
    public string? LastName { get; set; }
    
    [Required]
    [DataType(DataType.Password)] 
    public string? Password { get; set; }
    
    [Required]
    [DataType(DataType.Password)] 
    [Compare("Password", ErrorMessage = "Пароли не совпадают")] 
    public string? PasswordConfirm { get; set; }
}