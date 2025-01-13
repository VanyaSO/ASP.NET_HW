using Microsoft.AspNetCore.Identity;

namespace lesson_08_01_blog.Models;

public class User : IdentityUser
{
    public int PublicationsCount { get; set; }
    public string? Name { get; set; }
}