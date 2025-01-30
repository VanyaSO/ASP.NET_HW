namespace hw_20_dop_1;

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Book> Books { get; set; }
}