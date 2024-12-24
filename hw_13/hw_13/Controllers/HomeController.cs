using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hw_13.Models;

namespace hw_13.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}