using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace hw_7.Controllers;

public class TaskDop3Controller : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(UserRegisterModel user)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_Message", (message: "Произошла ошибка", isError: true));
        }   
        return PartialView("_Message", (message: $"{user.FullName}, вы успешно зарегистрировались", isError: false));
    }
}

public class UserRegisterModel
{
    public string FullName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}