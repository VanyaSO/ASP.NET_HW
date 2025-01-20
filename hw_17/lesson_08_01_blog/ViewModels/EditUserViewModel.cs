using System.ComponentModel.DataAnnotations;

namespace lesson_08_01_blog.ViewModels;

public class EditUserViewModel
{
    public string Id { get; set; }
    
    [Required]
    public string? Name { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Required]
    public  string Phone { get; set; }
}