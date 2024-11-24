using Microsoft.EntityFrameworkCore;

namespace hw_5_dop_2.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
    {
    }
    
    public DbSet<Course> Courses {get; set;}
    public DbSet<CourseSubscription> CourseSubscriptions {get; set;}
}