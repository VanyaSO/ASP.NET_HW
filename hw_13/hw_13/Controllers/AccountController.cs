using hw_13.Models;
using hw_13.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hw_13.Controllers;

public class AccountController : Controller
{
    private readonly UserService _users;
    private readonly IPasswordHasher<User> _passwordHasher;
    

    public AccountController(UserService service)
    {
        _users = service;
        _passwordHasher = new PasswordHasher<User>();
    }
    
    [Authorize]
    public IActionResult Index()
    {
        return Content("Account");
    }
    
    public IActionResult Auth()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return View((new LoginViewModel(), new RegisterViewModel()));
        }

        return View(nameof(Index));
    }
    
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            User? user = _users.GetUserByEmail(model.Email);
            if (user != null)
            {
                
                return RedirectToAction(nameof(Index));
            }

            ViewBag.IsRegisterAcive = true;
            return View("Auth", (new LoginViewModel(), new RegisterViewModel{Username = model.Email}));
        }
        
        return View("Auth", (new LoginViewModel(), new RegisterViewModel()));
    }
    
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (_users.IsUsernameRegister(model.Username.Trim()))
            {
                ModelState.AddModelError("Username", "Username is already in use");
                ViewBag.IsRegisterAcive = true;
                return View("Auth", (new LoginViewModel(), model));
            } 
            
            if (_users.IsEmailRegister(model.Email.Trim()))
            {
                ModelState.AddModelError("Email", "Email is already in use");
                ViewBag.IsRegisterAcive = true;
                return View("Auth", (new LoginViewModel(), model));
            }
            
            User user = new User()
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            user.Password = HashPassword(user, user.Password);
            _users.AddUser(user);
            
            return RedirectToAction(nameof(Login));
        }
        
        return View("Auth", (new LoginViewModel(), model));
    }
    
    public IActionResult IsUsernameRegister(string username) => Json(!_users.IsUsernameRegister(username));
    public IActionResult IsEmailRegister(string email) => Json(!_users.IsEmailRegister(email));
    
    private string HashPassword(User user, string password) => _passwordHasher.HashPassword(user, password);
    
    private bool VerifyPassword(User user, string enteredPassword, string storedHash)
    {
        try
        {
            Convert.FromBase64String(storedHash);
        }
        catch (FormatException)
        {
            return false;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, storedHash, enteredPassword);
        return result == PasswordVerificationResult.Success;
    }
}