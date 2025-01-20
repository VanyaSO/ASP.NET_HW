using lesson_08_01_blog.Models;

namespace lesson_08_01_blog.ViewModels;

public class GetPublicationViewModel
{
    public Publication Publication {get; set; }
    public string? ReturnUrl { get; set; }
}