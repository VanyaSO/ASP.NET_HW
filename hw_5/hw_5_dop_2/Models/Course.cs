namespace hw_5_dop_2.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int NumberSeats { get; set; }
    public int BusySeats { get; set; } = 30;
    
    public IEnumerable<CourseSubscription> Subscriptions { get; set; }
}