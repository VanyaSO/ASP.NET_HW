using hw_6.Models;

namespace hw_6.Interfaces;

public interface ICourse
{
    public List<Course> GetAll();
    public Course? GetCourseById(int id);
}