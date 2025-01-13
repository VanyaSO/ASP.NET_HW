using System.ComponentModel.DataAnnotations;

namespace lesson_08_01_blog.ViewModels;

public class RegisterUserViewModel
{
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string? Phone { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string PasswordConfirm { get; set; }
    
    public string Code { get; set; }
}