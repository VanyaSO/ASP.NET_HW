namespace hw_20_dop_1;

public class BookDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }

    public Guid GenreId { get; set; }
    public string GenreName { get; set; }
}
