using hw_5_dop_2.Models;

namespace hw_5_dop_2.Interface;

public interface ICourse
{
    public IEnumerable<Course> GetAll();
    public Course? GetCourseById(int id);
}