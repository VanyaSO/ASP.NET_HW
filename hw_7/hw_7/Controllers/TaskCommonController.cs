using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hw_7.Models;

namespace hw_7.Controllers;

public class TaskCommonController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}