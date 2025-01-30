namespace hw_20_dop_1;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Guid AuthorId { get; set; }
    public Author? Author { get; set; }

    public Guid GenreId { get; set; }
    public Genre? Genre { get; set; }
}
