namespace hw_5_dop_2.Models;

public class CourseSubscription
{
    public int Id { get; set; }
    public string Email { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
}