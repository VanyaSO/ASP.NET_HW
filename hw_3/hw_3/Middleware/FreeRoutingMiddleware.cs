using hw_3.Model;
using Microsoft.EntityFrameworkCore;

namespace hw_3.Middleware;

public class FreeRoutingMiddleware
{
    private readonly RequestDelegate _next;
    
    public FreeRoutingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context, ApplicationContext db)
    {
        string path = context.Request.Path;
        if (path == "/")
        {
            await context.Response.WriteAsync("Welcome");
        }
        else if (path == "/allbooks")
        {
            var books = await db.Books.Include(e => e.Category)
                .Select(e => new
                {
                    Name = e.Title,
                    Category = e.Category.Name
                }).ToListAsync(); 
            
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync(HtmlHelper.GenerateHtmlPage(HtmlHelper.BuildHtmlTable(books), "All books"));
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}