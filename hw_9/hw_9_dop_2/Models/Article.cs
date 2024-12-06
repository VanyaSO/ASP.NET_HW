namespace hw_9_dop_2.Models;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string TextContent { get; set; } = String.Empty;
    public string? Image { get; set; }
    public DateTime DateTimeCreate { get; set; }
    public int CategoryId { get; set; }
}