using hw_9_dop_2.Models;
using hw_9_dop_2.Services;
using Microsoft.AspNetCore.Mvc;

namespace hw_9_dop_2.Controllers;

public class ArticleController : Controller
{
    private readonly CategoryService _categories;
    private readonly ArticleService _articles;
    
    public ArticleController(CategoryService categories, ArticleService articles)
    {
        _categories = categories;
        _articles = articles;
    }

    public IActionResult Index(int id)
    {
        Article? article = _articles.GetArticleById(id);
        if (article is not null)
        {
            ArticleViewModel avm = new ArticleViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                TextContent = article.TextContent,
                DateTimeCreate = article.DateTimeCreate,
                Image = article.Image,
                CategoryName = _categories.GetCategoryById(article.CategoryId)?.Title
            };
            return View(avm);
        }

        return NotFound("Статья не найдена");
    }
}