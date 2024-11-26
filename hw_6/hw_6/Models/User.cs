namespace hw_6.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public  string Phone { get; set; }
    public string Token { get; set; }
    public List<Course> Courses { get; set; }
}