using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace hw_14.ViewModel;

public class RegisterViewModel
{
    [Required]
    [Remote(action: "IsUsernameRegister", controller: "Account")]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    [Remote(action: "IsEmailRegister", controller: "Account")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}