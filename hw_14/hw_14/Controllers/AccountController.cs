using System.Security.Claims;
using hw_14.Filters;
using hw_14.Heplers;
using hw_14.Models;
using hw_14.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hw_14.Controllers;

public class AccountController : Controller
{
    private readonly UserService _users;
    
    public AccountController(UserService service)
    {
        _users = service;
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
    [LogActionFilter(Description = "user logged in")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var logFilter = HttpContext.Items["LogActionFilter"] as LogActionFilter;

        if (ModelState.IsValid)
        {
            User? user = _users.GetUserByEmail(model.Email);
            if (user != null)
            {
                if (PasswordHelper.VerifyPassword(user, model.Password))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("currencyId", user.ExchangeRateId.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookie");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    logFilter.UserEmail = model.Email;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Password", "Incorrect password");
            }
            else
                ModelState.AddModelError("Email", "User not found");
        }

        if (logFilter is not null)
        {
            logFilter.UserEmail = model.Email;
            logFilter.Description = "authorization failure";
        }
        
        return View("Auth", (new LoginViewModel(), new RegisterViewModel()));
    }
    
    [HttpPost]
    [LogActionFilter(Description = "user registered")]
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

                user.Password = PasswordHelper.HashPassword(user);
                _users.AddUser(user);
            
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.IsRegisterAcive = true;
        }

        var logFilter = HttpContext.Items["LogActionFilter"] as LogActionFilter;
        if (logFilter is not null)
        {
            logFilter.UserEmail = model.Email;
            logFilter.Description = "registration failure";
        }
        return View("Auth", (new LoginViewModel(), model));
    }
    
    public IActionResult IsUsernameRegister(string username) => Json(!_users.IsUsernameRegister(username));
    public IActionResult IsEmailRegister(string email) => Json(!_users.IsEmailRegister(email));
    
    [LogActionFilter(Description = "user logged out")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Index));
    }
}