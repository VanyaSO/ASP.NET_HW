using System.Security.Claims;
using hw_13.Models;
using hw_13.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        var email = HttpContext.User.FindFirst(ClaimTypes.Email);
        return View(_users.GetUserByEmail(email.Value));
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
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            User? user = _users.GetUserByEmail(model.Email);
            if (user != null)
            {
                if (VerifyPassword(user, model.Password))
                {
                    var claims = new List<Claim>() { new Claim(ClaimTypes.Email, user.Email) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookie");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Password", "Incorrect password");
            }
            else
                ModelState.AddModelError("Email", "User not found");
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
            } else if (_users.IsEmailRegister(model.Email.Trim()))
            {
                ModelState.AddModelError("Email", "Email is already in use");
            }
            else
            {
                User user = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };

                user.Password = HashPassword(user);
                _users.AddUser(user);
            
                return RedirectToAction(nameof(Auth));
            }
            
            ViewBag.IsRegisterAcive = true;
        }
        
        return View("Auth", (new LoginViewModel(), model));
    }
    
    public IActionResult IsUsernameRegister(string username) => Json(!_users.IsUsernameRegister(username));
    public IActionResult IsEmailRegister(string email) => Json(!_users.IsEmailRegister(email));
    
    private string HashPassword(User user) => _passwordHasher.HashPassword(user, user.Password);
    
    private bool VerifyPassword(User user, string enteredPassword)
    {
        try
        {
            Convert.FromBase64String(user.Password);
        }
        catch (FormatException)
        {
            return false;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, enteredPassword);
        return result == PasswordVerificationResult.Success;
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Index));
    }
}