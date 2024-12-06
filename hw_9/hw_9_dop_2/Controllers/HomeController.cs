using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hw_9_dop_2.Models;
using hw_9_dop_2.Services;
using hw_9_dop_2.ViewModels;

namespace hw_9_dop_2.Controllers;

public class HomeController : Controller
{
    private readonly CategoryService _categories;
    private readonly ArticleService _articles;
    
    public HomeController(CategoryService categories, ArticleService articles)
    {
        _categories = categories;
        _articles = articles;
    }
    
    public IActionResult Index()
    {
        List<CategoryViewModel> categories = _categories
            .GetAllCategories()
            .Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CountArticles = _articles.GetArticlesByCategoryId(c.Id).Count()
            })
            .ToList();

        categories.Insert(0, new CategoryViewModel()
        {
            Id = 0,
            Title = "Все",
            Description = "",
            CountArticles = _articles.GetAllArticles().Count()
        });
        
        return View(categories);
    }
}