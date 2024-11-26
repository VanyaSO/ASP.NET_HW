using hw_6.Interfaces;
using hw_6.Models;

namespace hw_6.Repositories;

public class UserCourseRepository : IUserCourse
{
    private readonly ApplicationContext _context;

    public UserCourseRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public void AddRegistration(UserCourse userCourse)
    {
        if (!IsUserRegisteredToCourse(userCourse.UserId, userCourse.CourseId))
        {
            _context.UserCourse.Add(userCourse);
            _context.SaveChanges();   
        }
    }

    private bool IsUserRegisteredToCourse(int userId, int courseId) => _context.UserCourse
        .Any(uc => uc.UserId == userId && uc.CourseId == courseId);
}