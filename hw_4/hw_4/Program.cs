using System.Data.SqlClient;
using hw_4.Model;
using hw_4.Helpers;
using hw_4.Interfaces;
using hw_4.Repository;

var builder = WebApplication.CreateBuilder();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IUser, UserRepository>(provider => new UserRepository(connectionString));
builder.Services.AddScoped<Database>(provider => new Database(connectionString));

var app = builder.Build();

// создайте таблицу :)
// CREATE TABLE [dbo].[Users] (
//     [Id]   INT            IDENTITY (1, 1) NOT NULL,
//     [Name] NVARCHAR (MAX) NOT NULL,
//     [Age]  INT            NOT NULL,
//     CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
// );

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    response.ContentType = "text/html; charset=utf-8";
    if (request.Path == "/")
    {
        var repository = context.RequestServices.GetService<IUser>();
        var users = await repository.GetAllUsers();

        await response.WriteAsync(BuildHtml.GenerateHtmlPage(BuildHtml.BuildHtmlTable(users), "All Users from DataBase"));   
    }
    else if (request.Path == "/delete")
    {
        if (Int32.TryParse(request.Query["id"], out int id))
        {
            var repository = context.RequestServices.GetService<IUser>();
            await repository.DeleteUserById(id);
        }

        response.Redirect("/");
    }
    else if (request.Path == "/update")
    {
        int id = Int32.Parse(request.Form["id"]);
        string name = request.Form["name"];
        int age = Int32.Parse(request.Form["age"]);
        
        var repository = context.RequestServices.GetService<IUser>();
        await repository.UpdateUser(new User() {Id = id, Name = name, Age = age});

        response.Redirect("/");
    }
    else if (request.Path == "/search" && request.Method == "GET")
    {
        string option = request.Query["option"];
        string value = request.Query["value"].ToString().Trim();

        var repository = context.RequestServices.GetService<IUser>();
        var users = await repository.SearchUsers(option, value);

        await response.WriteAsync(BuildHtml.GenerateHtmlPage(BuildHtml.BuildHtmlTable(users), "Find users"));
    }
    else if (request.Path == "/init")
    {
        await context.RequestServices.GetService<Database>().Init();
        response.Redirect("/");
    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync("Page Not Found");
    }
});

app.Run();