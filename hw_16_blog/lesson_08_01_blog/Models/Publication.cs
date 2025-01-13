namespace lesson_08_01_blog.Models;

public class Publication
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? FullImageName { get; set; }
    public string? Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? SeoDescription { get; set; }
    public string? Keywords { get; set; }
    public long TotalViews { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
}