using Microsoft.AspNetCore.Identity;

namespace hw_18.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Subject> Subjects { get; set; }
    public ICollection<Grade> Grades { get; set; }
}