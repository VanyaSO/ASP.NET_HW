using lesson_08_01_blog.Models;

namespace lesson_08_01_blog.ViewModels;

public class ContentViewModel
{
    public IEnumerable<Category> Categories { get; set; }
    public IEnumerable<Publication> Publications { get; set; }
}