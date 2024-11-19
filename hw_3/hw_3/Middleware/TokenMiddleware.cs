using hw_3.Model;
using Microsoft.EntityFrameworkCore;

namespace hw_3.Middleware;

// Представим, что у вас есть минимальное API в ASP.NET Core, которое позволяет пользователям получать список книг из базы данных,
// при обращении по адресу “/allbooks”. 
// Однако, вы хотите добавить функциональность, которая позволит вам фильтровать список книг по категориям
// только если пользователь авторизован, по адресу “/getbooks?token=token12345&category=music”.
// В данном случае, пользователь будет считаться авторизованным, если у него в запросе присутствует token, с определенным значением.
// Вы должны использовать собственный класс Middleware.
// Книги возвращать в виде HTML таблицы.


public class TokenMiddleware
{
    private readonly RequestDelegate _next;
    
    public TokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context, ApplicationContext db)
    {
        if (context.Request.Path == "/getbooks")
        {
            string token = context.Request.Query["token"];
            if (token == "token12345")
            {
                string category = context.Request.Query["category"];
                var books = await db.Books
                    .Where(e => e.Category.Name == category)
                    .Select(e => new {
                        Book = e.Title,
                        Category = e.Category.Name
                    })
                    .ToListAsync();
                
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync(HtmlHelper.GenerateHtmlPage(HtmlHelper.BuildHtmlTable(books), $"Books from category {category}"));
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");
            }
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}