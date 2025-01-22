using Microsoft.AspNetCore.Mvc;

namespace hw_18.Controllers;

public class HomeController : Controller
{

    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }
}