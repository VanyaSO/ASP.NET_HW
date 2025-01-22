using hw_18.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hw_18.Data;

public static class InitializerData
{
    public static async Task InitializeAsync(ApplicationContext context, UserManager<User> userManager, EmailsConfig emailsConfig)
    {
        if (!context.Subjects.Any())
        {
            var subjects = new[]
            {
                new Subject() { Name = "C#" },
                new Subject() { Name = "C++" },
                new Subject() { Name = "Asp.Net" },
                new Subject() { Name = "Java Script" }
            };

            await context.Subjects.AddRangeAsync(subjects);
            await context.SaveChangesAsync();   
        }
        
        if (!userManager.Users.Any())
        {
            var users = new[]
            {
                new
                {
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Password = "qwerty",
                },
                new
                {
                    Email = "ushachovg324@gmail.com",
                    FirstName = "Ivan",
                    LastName = "Ushachov",
                    Password = "qwerty",
                },
                new
                {
                    Email = "alex@gmail.com",
                    FirstName = "Alex",
                    LastName = "Johnson",
                    Password = "DS(A)DS",
                },
                new
                {
                    Email = "marry@in.ua",
                    FirstName = "Marry",
                    LastName = "Williams",
                    Password = "q1d561SD",
                },
                new
                {
                    Email = "tom@ukr.net",
                    FirstName = "Tom",
                    LastName = "Brown",
                    Password = "12312DSAss",
                }
            };

            foreach (var user in users)
            {
                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    User currentUser = new User
                    {
                        Email = user.Email,
                        UserName = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };

                    var result = await userManager.CreateAsync(currentUser, user.Password);

                    if (result.Succeeded && !emailsConfig.Emails.Contains(currentUser.Email))
                    {
                        var userSubjects = await context.Subjects.ToListAsync();
                        currentUser.Subjects = userSubjects;
                        await context.SaveChangesAsync();
                    }
                }
            }
        }

        if (!context.Grades.Any())
        {
            var subjects = await context.Subjects.ToListAsync();
            var users = await context.Users.ToListAsync();

            var grades = new List<Grade>()
            {
                new Grade()
                {
                    Numbers = new List<int>() {9},
                    SubjectId = subjects[1].Id,
                    UserId = users[0].Id
                },
                new Grade()
                {
                    Numbers = new List<int>() {7},
                    SubjectId = subjects[1].Id,
                    UserId = users[0].Id
                },
                new Grade()
                {
                    Numbers = new List<int>() {12},
                    SubjectId = subjects[2].Id,
                    UserId = users[0].Id
                },
                new Grade()
                {
                    Numbers = new List<int>() {10},
                    SubjectId = subjects[3].Id,
                    UserId = users[0].Id
                },
                new Grade()
                {
                    Numbers = new List<int>() {8},
                    SubjectId = subjects[1].Id,
                    UserId = users[1].Id
                },
                new Grade()
                {
                    Numbers = new List<int>() {6},
                    SubjectId = subjects[1].Id,
                    UserId = users[0].Id
                },
                new Grade()
                {
                    Numbers = new List<int>() {11},
                    SubjectId = subjects[3].Id,
                    UserId = users[1].Id
                },
                new Grade()
                {
                    Numbers = new List<int>() {9},
                    SubjectId = subjects[3].Id,
                    UserId = users[2].Id
                }
            };

            await context.Grades.AddRangeAsync(grades);
            await context.SaveChangesAsync();
        }
    }
}