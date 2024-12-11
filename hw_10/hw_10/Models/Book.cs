namespace hw_10.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    public string Year { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    
    public ICollection<Review> Reviews { get; set; }
}