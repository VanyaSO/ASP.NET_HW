using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hw_10_dop_2.Models;

namespace hw_10_dop_2.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string? city)
    {
        return View(nameof(Index), city);
    }
}