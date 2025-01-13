using lesson_08_01_blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace lesson_08_01_blog.Data;

public class RoleInitializer
{
    public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            //Добавляем роли
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("Editor") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Editor"));
            }
        }
 
        if (!userManager.Users.Any())
        {
            //Добавляем пользователей, через анонимный массив
            var users = new[]
            {
                new { Email = "admin@gmail.com", Name = "Admin", Password = "qwerty"  },
                new { Email = "alex@gmail.com", Name = "Alex", Password = "DS(A)DS"  },
                new { Email = "marry@in.ua", Name = "Marry", Password = "q1d561SD"  },
                new { Email = "tom@ukr.net", Name = "Tom", Password = "12312DSAss"  },
                new { Email = "john@gmail.com", Name = "John", Password = "ds0012sd"  }
            };
 
            foreach (var user in users)
            {
                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    User currentUser = new User { Email = user.Email, UserName = user.Email, Name = user.Name };
                    IdentityResult result = await userManager.CreateAsync(currentUser, user.Password);
                    if (result.Succeeded)
                    {
                        if (currentUser.Email.Equals("admin@gmail.com"))
                        {
                            await userManager.AddToRoleAsync(currentUser, "Admin");
                        }
                        else
                        {
                            await userManager.AddToRoleAsync(currentUser, "Editor");
                        }
                    }
                }
            }
        }
    }
}