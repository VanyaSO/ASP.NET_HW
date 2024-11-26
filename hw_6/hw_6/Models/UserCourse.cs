namespace hw_6.Models;

public class UserCourse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public int CourseId { get; set; }
    public Course? Course { get; set; }
    
    public DateTime DateRegister { get; set; }
}