namespace hw_10.Models;

public class BookCardViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
}