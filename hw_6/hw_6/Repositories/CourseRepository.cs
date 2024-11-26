using hw_6.Interfaces;
using hw_6.Models;
using Microsoft.EntityFrameworkCore;

namespace hw_6.Repositories;

public class CourseRepository : ICourse
{
    private readonly ApplicationContext _context;

    public CourseRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public List<Course> GetAll() => _context.Courses.Include(c => c.Users).ToList();
    public Course? GetCourseById(int id) => _context.Courses.FirstOrDefault(e => e.Id == id);
}