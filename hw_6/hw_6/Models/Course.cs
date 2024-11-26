namespace hw_6.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public List<User> Users { get; set; }
}