using hw_11.Models;
using hw_11.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hw_11.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationContext _context;

    public AccountController(ApplicationContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Register(RegisterViewModel rvm)
    {
        if (ModelState.IsValid)
        {
            if (CheckIsUsernameInUse(rvm.Username))
            {
                ModelState.AddModelError("Username", "Username is already in use");
                return View(rvm);
            }
            
            User newUser = new User()
            {
                FirstName = rvm.FirstName,
                LastName = rvm.LastName,
                Age = rvm.Age,
                Username = rvm.Username,
                Email = rvm.Email,
                PhoneNumber = rvm.PhoneNumber,
                Password = rvm.Password,
                CreditCardNumber = rvm.CreditCardNumber,
                WebSite = rvm.WebSite,
                TermsOfService = rvm.TermsOfService
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Redirect($"Home/Index");
        }
        return View(rvm);
    }

    [AllowAnonymous]
    public IActionResult IsUsernameInUse(string username) => Json(!CheckIsUsernameInUse(username));

    private bool CheckIsUsernameInUse(string username) => _context.Users.Any(u => u.Username.Equals(username));
}