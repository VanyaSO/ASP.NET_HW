using lesson_08_01_blog.Models;

namespace lesson_08_01_blog.Data;

public class CommonInitializer
{
    public static async Task InitializeAsync(ApplicationContext context)
    {
        if (!context.Subscribers.Any())
        {
            context.Subscribers.AddRange
            (
                new Subscriber() { Email = "ushachov324@gmail.com" }
            );
            context.SaveChanges();
        }
    }
}