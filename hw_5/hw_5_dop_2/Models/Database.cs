using Microsoft.EntityFrameworkCore;

namespace hw_5_dop_2.Models;

public class Database
{
    private readonly ApplicationContext _context;

    public Database(ApplicationContext context)
    {
        _context = context;
    }

    public void Init()
    {
        _context.Database.EnsureDeleted();
        _context.Database.Migrate();

        if (!_context.Courses.Any())
        {
            var courses = new[]
            {
                new Course
                {
                    Name = "Introduction to Programming",
                    Description =
                        "Learn the basics of programming with an emphasis on problem-solving and logical thinking.",
                    NumberSeats = 30,
                    Subscriptions = new List<CourseSubscription>()
                },
                new Course
                {
                    Name = "Advanced Web Development",
                    Description =
                        "Dive deep into modern web technologies, frameworks, and best practices for building responsive and dynamic websites.",
                    NumberSeats = 25,
                    Subscriptions = new List<CourseSubscription>()
                },
                new Course
                {
                    Name = "Data Science and Machine Learning",
                    Description =
                        "Explore data analysis techniques and machine learning algorithms to gain insights from data.",
                    NumberSeats = 20,
                    Subscriptions = new List<CourseSubscription>()
                },
                new Course
                {
                    Name = "Mobile App Development",
                    Description =
                        "Learn how to develop mobile applications for both iOS and Android using cross-platform frameworks.",
                    NumberSeats = 40,
                    Subscriptions = new List<CourseSubscription>()
                },
                new Course
                {
                    Name = "Cybersecurity Fundamentals",
                    Description =
                        "Understand the core concepts of cybersecurity, including risk management, encryption, and ethical hacking.",
                    NumberSeats = 35,
                    Subscriptions = new List<CourseSubscription>()
                }
            };


            _context.Courses.AddRange(courses);
            _context.SaveChanges();
        }
    }
}