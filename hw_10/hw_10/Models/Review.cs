namespace hw_10.Models;

public class Review
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
    public string UserName { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
}