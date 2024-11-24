using hw_5_dop_2.Interface;
using hw_5_dop_2.Models;

namespace hw_5_dop_2.Repositories;

public class CourseSubscriptionRepository : ICourseSubscription
{
    private readonly ApplicationContext _context;

    public CourseSubscriptionRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public void AddSubscribe(CourseSubscription sub)
    {
        Course? course = _context.Courses.FirstOrDefault(e => e.Id == sub.CourseId);
        if (course is null)
        {
            throw new ArgumentException("Course not found!");
        }
        else if (IsSubscribedToCourse(sub.CourseId, sub.Email))
        {
            throw new InvalidOperationException("You are registered for this course");
        }
        else if (course.BusySeats == course.NumberSeats)
        {
            throw new InvalidOperationException("The course has no available places");
        }
        
        _context.CourseSubscriptions.Add(sub);
        
        course.BusySeats += 1;
        _context.Courses.Update(course);
        _context.SaveChanges();
    }

    private bool IsSubscribedToCourse(int id, string email) => _context.CourseSubscriptions
        .Any(e => e.CourseId == id && e.Email == email);
}