using System.ComponentModel.DataAnnotations;

namespace lesson_08_01_blog.ViewModels;

public class ChangePasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    public string? OldPassword { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string? NewPassword { get; set; }
}