namespace hw_19.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    
    public User? User { get; set; }
}