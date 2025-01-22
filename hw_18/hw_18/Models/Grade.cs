using hw_18.Models;

public class Grade
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public List<int> Numbers { get; set; }

    public string UserId { get; set; }
    public User? User { get; set; }

    public string SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public DateTime? CreatedAt { get; set; }
}