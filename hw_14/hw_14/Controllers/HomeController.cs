using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hw_14.Models;

namespace hw_14.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}