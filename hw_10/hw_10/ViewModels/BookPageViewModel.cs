using hw_10.Models;

namespace hw_10.ViewModels;

public class BookPageViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Year { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    
    public List<Review> Reviews { get; set; }
}