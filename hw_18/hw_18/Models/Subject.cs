namespace hw_18.Models;

public class Subject
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<Grade> Grades { get; set; }
}