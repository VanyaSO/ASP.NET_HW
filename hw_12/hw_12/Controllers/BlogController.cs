using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hw_12.Models;

namespace hw_12.Controllers;

public class BlogController : Controller
{
    private List<Article> _news = new List<Article>()
    {
        new Article
        {
            Title = "Новость о технологиях",
            Description = "Краткое описание последнего достижения в ИИ.",
            CrateAt = DateTime.Now,
            MinutesForRead = 5
        },
        new Article
        {
            Title = "Новости спорта",
            Description = "Успех местной футбольной команды.",
            CrateAt = DateTime.Now.AddMinutes(-30),
            MinutesForRead = 3
        }
    };
    
    public IActionResult Index()
    {
        return View(_news);
    }

    [HttpPost]
    public IActionResult SetTheme(string theme)
    {
        Response.Cookies.Append("theme", theme);
        return RedirectToAction(nameof(Index));
    }

}