using lesson_08_01_blog.Models;

namespace lesson_08_01_blog.Data;

public class ContentInitializer
{
    private readonly static Random _random = new Random();
    public static async Task InitializeAsync(ApplicationContext context)
{
    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category { Name = "Путешествия", Description = "Путешествия по всему земному шару." },
            new Category { Name = "Истории", Description = "Рассказанная история — это прожитая жизнь." },
            new Category { Name = "Фильмы", Description = "Всё! Кина не будет! Электричество кончилось!" }
        );
        await context.SaveChangesAsync();
    }

    if (!context.Publications.Any())
    {
        var users = context.Users.ToList();
        var randomUser = users.OrderBy(_ => _random.Next()).FirstOrDefault();

        if (randomUser == null)
        {
            throw new InvalidOperationException("Unable to find a random user.");
        }

        context.Publications.AddRange(
            new Publication
            {
                Title = "Детройт: хроники «мёртвого» города",
                Description = "А вам слабо взять и экспромтом поехать в Детройт...",
                Categories = new List<Category>
                {
                    context.Categories.FirstOrDefault(e => e.Name == "Путешествия")
                },
                SeoDescription = "Детройт: хроники «мёртвого» города",
                Keywords = "поездка в детройт",
                UserId = randomUser.Id
            },
            new Publication
            {
                Title = "Достичь успеха, меняя образ",
                Description = "Когда в городе укрепилось высокое мнение...",
                Categories = new List<Category>
                {
                    context.Categories.FirstOrDefault(e => e.Name == "Истории"),
                    context.Categories.FirstOrDefault(e => e.Name == "Путешествия")
                },
                SeoDescription = "Достичь успеха, меняя образ. Занимательная история",
                Keywords = "успех в жизни",
                UserId = randomUser.Id
            }
        );

        await context.SaveChangesAsync();
    }
}

}