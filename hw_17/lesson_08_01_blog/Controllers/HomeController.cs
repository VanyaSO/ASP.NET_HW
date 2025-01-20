using System.Diagnostics;
using lesson_08_01_blog.Interfaces;
using Microsoft.AspNetCore.Mvc;
using lesson_08_01_blog.Models;
using lesson_08_01_blog.Repositories;
using lesson_08_01_blog.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace lesson_08_01_blog.Controllers;

public class HomeController : Controller
{
    public readonly ICategory _categories;
    public readonly IPublication _publications;
    public readonly IUser _users;
    
    public HomeController(ICategory categories, IPublication publications, IUser users)
    {
        _categories = categories;
        _publications = publications;
        _users = users;
    }

    public async Task<IActionResult> Index(QueryOptions? options, string? categoryId)
    {
        var allCategories = await _categories.GetAllCategoriesAsync();
        var allPublications = await _publications.GetAllPublicationsByCategoriesWithUserAsync(options, categoryId);

        return View(new IndexViewModel()
        {
            Categories = allCategories.ToList(),
            Publications = allPublications
        });
    }

    [Route("publication")]
    public async Task<IActionResult> GetPublication(string? id, string? returnUrl)
    {
        var currentPublication = await _publications.GetPublicationWithCategoriesWithUserAsync(id);
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
    
    public async Task<IActionResult> GetUser(string? username)
    {
        if (!string.IsNullOrEmpty(username))
        {
            var user = await _users.GetUserWithPublicationsByUserNameAsync(username);
            return View(user);
        }

        return NotFound();
    }
}