using hw_20_dop_1;
using hw_20_dop_1.Data;
using hw_21.Models;
using Microsoft.EntityFrameworkCore;

public static class Initializer
{
    public static void Init(ApplicationContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.AddRange(new List<User>
            {
                new User { Email = "ushachovg324@gmail.com", Password = "pass1", Username = "UserOne" },
                new User { Email = "user2@example.com", Password = "pass2", Username = "UserTwo" },
                new User { Email = "user3@example.com", Password = "pass3", Username = "UserThree" },
                new User { Email = "user4@example.com", Password = "pass4", Username = "UserFour" },
                new User { Email = "user5@example.com", Password = "pass5", Username = "UserFive" }
            });
            context.SaveChanges();
        }
    }
}