using System.ComponentModel.DataAnnotations;

namespace hw_13.ViewModel;

public class RegisterViewModel
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}