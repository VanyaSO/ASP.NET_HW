using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace hw_11.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "This field is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "This field is required")]
    public string LastName { get; set; }
    
    [Range(minimum: 18, maximum: 100, ErrorMessage = "Specify age between 18 and 100")]
    public int Age { get; set; }

    [RegularExpression(@"^[A-Za-z0-9_]+$", ErrorMessage = "This field can only contain letters, numbers, and underscores")]
    [StringLength(20, ErrorMessage = "Maximum length is 20 characters.")]
    [Remote(action: "IsUsernameInUse", controller: "Account", ErrorMessage = "Username is already in use")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [StringLength(100, ErrorMessage = "Maximum length is 20 characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
    
    [CreditCard(ErrorMessage = "Invalid card number")]
    public string? CreditCardNumber { get; set; }
    
    [Url(ErrorMessage = "Does not match the URL format")]
    public string? WebSite { get; set; }
    
    [ValidateNever]
    public bool TermsOfService { get; set; }
}