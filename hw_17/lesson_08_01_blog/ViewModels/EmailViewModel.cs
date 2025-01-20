using System.ComponentModel.DataAnnotations;

namespace lesson_08_01_blog.ViewModels;

public class EmailViewModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
}