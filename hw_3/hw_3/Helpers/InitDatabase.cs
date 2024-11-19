using hw_3.Model;
using Microsoft.EntityFrameworkCore;

namespace hw_3.Helpers;

public static class InitDatabase
{
    public static void Init(ApplicationContext context)
    {
        context.Database.EnsureCreated();
        
        if (!context.Categories.Any() && !context.Books.Any())
        {
            var category1 = new Category { Name = "Programming" };
            var category2 = new Category { Name = "Fiction" };

            context.Categories.AddRange(category1, category2);
            context.SaveChanges();

            var book1 = new Book { Title = "C# Programming", CategoryId = category1.Id };
            var book2 = new Book { Title = "The Hobbit", CategoryId = category2.Id };
            var book3 = new Book { Title = "C++", CategoryId = category1.Id };

            context.Books.AddRange(book1, book2, book3);
            context.SaveChanges();
        }
    }
}