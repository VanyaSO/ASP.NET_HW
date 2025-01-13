using lesson_08_01_blog.Models;

namespace lesson_08_01_blog.ViewModels;

public class IndexViewModel
{
    public IEnumerable<Publication> Publications { get; set; }
    public List<Category> Categories { get; set; }
}