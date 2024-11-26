using Microsoft.EntityFrameworkCore;

namespace hw_6.Models;

public class Database
{
    private readonly ApplicationContext _context;

    public Database(ApplicationContext context)
    {
        _context = context;
    }

    public void Init()
    {
        // _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        if (!_context.Users.Any())
        {
            var courses = new List<Course>
            {
                new Course 
                { 
                    Name = "Introduction to C#", 
                    Description = "Learn the basics of C# programming.", 
                    DateStart = new DateTime(2024, 12, 1), 
                    DateEnd = new DateTime(2024, 12, 15) 
                },
                new Course 
                { 
                    Name = "Advanced ASP.NET Core", 
                    Description = "Dive deep into ASP.NET Core and build robust web applications.", 
                    DateStart = new DateTime(2025, 1, 10), 
                    DateEnd = new DateTime(2025, 1, 20) 
                },
                new Course 
                { 
                    Name = "Entity Framework Core Essentials", 
                    Description = "Master Entity Framework Core for data access in .NET.", 
                    DateStart = new DateTime(2025, 2, 5), 
                    DateEnd = new DateTime(2025, 2, 15) 
                }
            };
            
            _context.Courses.AddRange(courses);
            _context.SaveChanges();
        }
    }
}