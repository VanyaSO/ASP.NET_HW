using hw_2.Model;
using hw_2.Helpers;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

var configurationService = app.Services.GetService<IConfiguration>();
string connectionString = configurationService["ConnectionStrings:DefaultConnection"];
string[] searchOptions = new[] { "Name", "Age" };

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    response.ContentType = "text/html; charset=utf-8";
    if (request.Path == "/")
    {
        List<User> users = new List<User>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand("select Id, Name, Age from UsersMy", connection);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
            }
        }

        await response.WriteAsync(
            BuildHtml.GenerateHtmlPage(BuildHtml.BuildHtmlTable(users), "All Users from DataBase"));
    }
    else if (request.Path == "/delete")
    {
        if (Int32.TryParse(request.Query["id"], out int itemId))
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand($"delete from UsersMy where Id = {itemId}", connection);

                await command.ExecuteNonQueryAsync();
            }
        }

        response.Redirect("/");
    }
    else if (request.Path == "/update")
    {
        int id = Int32.Parse(request.Form["id"]);
        string name = request.Form["name"];
        int age = Int32.Parse(request.Form["age"]);
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            
            SqlCommand command;
            if (id == 0)
            {
                command = new SqlCommand($"insert into UsersMy ([Name], [Age]) values ('{name}', {age})", connection);
            } 
            else
            {
                command = new SqlCommand($"UPDATE UsersMy SET [Name] = '{name}', [Age] = '{age}' WHERE Id = '{id}'", connection);
            }
            
            await command.ExecuteNonQueryAsync();
        }

        response.Redirect("/");
    }
    else if (request.Path == "/search" && request.Method == "GET")
    {
        string option = request.Query["option"];
        string value = request.Query["value"].ToString().Trim();

        List<User> users = new List<User>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand($"select Id, Name, Age from UsersMy WHERE {option} = '{value}'", connection);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
            }
        }

        await response.WriteAsync(BuildHtml.GenerateHtmlPage(BuildHtml.BuildHtmlTable(users), "Find users"));

    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync("Page Not Found");
    }
});

app.Run();