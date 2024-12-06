using hw_9_dop_2.Models;
using hw_9_dop_2.Services;
using hw_9_dop_2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace hw_9_dop_2.Controllers;

public class CategoryController : Controller
{
    private readonly CategoryService _categories;
    private readonly ArticleService _articles;
    
    public CategoryController(CategoryService categories, ArticleService articles)
    {
        _categories = categories;
        _articles = articles;
    }
    
    public IActionResult Index(int id)
    {
        IEnumerable<Article> articles = new List<Article>();
        CategoryViewModel сvm;
        
        if (id == 0)
        {
            articles = _articles.GetAllArticles();
            сvm = new CategoryViewModel()
            {
                Id = 0,
                Title = "Все",
                Description = "Статьи всех категорий",
                CountArticles = _articles.GetAllArticles().Count()
            };
        }
        else
        {
            Category? category = _categories.GetCategoryById(id);
            if (category is not null)
            {
                articles = _articles.GetArticlesByCategoryId(category.Id);
                сvm = new CategoryViewModel()
                {
                    Id = category.Id,
                    Title = category.Title,
                    Description = category.Description.PadLeft(300),
                    CountArticles = _articles.GetAllArticles().Count()
                };
            }
            else
            {
                return NotFound("Категория не найдена");
            }
        }
        
        ViewBag.Articles = articles.Select(a => new ArticleViewModel()
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            TextContent = a.TextContent,
            DateTimeCreate = a.DateTimeCreate,
            Image = a.Image,
            CategoryName = _categories.GetCategoryById(a.CategoryId)?.Title
        });
        
        return View(сvm);
    }
}