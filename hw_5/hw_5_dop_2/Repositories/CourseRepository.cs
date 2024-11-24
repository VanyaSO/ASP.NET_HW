using hw_5_dop_2.Interface;
using hw_5_dop_2.Models;

namespace hw_5_dop_2.Repositories;

public class CourseRepository : ICourse
{
    private readonly ApplicationContext _context;

    public CourseRepository(ApplicationContext context)
    {
        _context = context;
    }

    public IEnumerable<Course> GetAll() => _context.Courses.ToList();

    public Course? GetCourseById(int id) => _context.Courses.FirstOrDefault(e => e.Id == id);
}