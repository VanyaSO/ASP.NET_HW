using System.ComponentModel.DataAnnotations;

namespace lesson_08_01_blog.ViewModels;

public class EmailNewsletterViewModel
{
    public List<string> Emails { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Content { get; set; }
}