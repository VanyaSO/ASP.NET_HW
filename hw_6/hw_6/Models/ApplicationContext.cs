using hw_6.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<UserCourse> UserCourse { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Users)
            .WithMany(u => u.Courses)
            .UsingEntity<UserCourse>(
                uc => uc.ToTable("UserCourses")
            );
    }

}