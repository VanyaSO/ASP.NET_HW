using System.ComponentModel.DataAnnotations;

namespace lesson_08_01_blog.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введите емейл")]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [DataType(DataType.Password)]

public string? Password { get; set; }
    public bool RememberMe { get; set; }
    
    public string? ReturnUrl { get; set; }
}