using System.Diagnostics;
using lesson_08_01_blog.Interfaces;
using Microsoft.AspNetCore.Mvc;
using lesson_08_01_blog.Models;
using lesson_08_01_blog.ViewModels;

namespace lesson_08_01_blog.Controllers;

public class HomeController : Controller
{
    public readonly ICategory _categories;
    public readonly IPublication _publications;
    public readonly IWebHostEnvironment _appEnvironment;
    
    public HomeController(ICategory categories, IPublication publications, IWebHostEnvironment appEnvironment)
    {
        _categories = categories;
        _publications = publications;
        _appEnvironment = appEnvironment;
    }

    public async Task<IActionResult> Index(QueryOptions? options, string? categoryId)
    {
        var allCategories = await _categories.GetAllCategoriesAsync();
        var allPublications = await _publications.GetAllPublicationsByCategoriesAsync(options, categoryId);

        return View(new IndexViewModel()
        {
            Categories = allCategories.ToList(),
            Publications = allPublications
        });
    }

    [Route("publication")]
    public async Task<IActionResult> GetPublication(string? id, string? returnUrl)
    {
        var currentPublication = await _publications.GetPublicationWithCategoriesAsync(id);
        if (currentPublication is not null)
        {
            await _publications.UpdateViewsAsync(currentPublication.Id.ToString());
            return View(new GetPublicationViewModel()
            {
                Publication = currentPublication,
                ReturnUrl = returnUrl
            });
        }

        return NotFound();
    }
}