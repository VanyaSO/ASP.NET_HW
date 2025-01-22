using System.Security.Claims;
using hw_18.Helpers;
using hw_18.Interfaces;
using hw_18.Models;
using hw_18.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hw_18.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Route("/account")]
    [Authorize]
    public IActionResult Index()
    {
        if (!HttpContext.User.IsHasAccess(HttpContext))
        {
            string? email = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if (!string.IsNullOrEmpty(email))
            {
                User user = _userManager.Users
                    .Include(u => u.Subjects)
                    .ThenInclude(s => s.Grades)
                    .FirstOrDefault(u => u.Email.Equals(email));

                if (user is not null)
                {
                    return View(new UserViewModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Subjects = user.Subjects
                    });
                }
            }
        }

        return NotFound();
    }

    [Route("/register")]
    public IActionResult Register()
    {
        return View();
    }

    [Route("/register")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userCheck = await _userManager.FindByEmailAsync(model.Email);
            if (userCheck != null)
            {
                ModelState.AddModelError("Email", "Такой email адрес уже есть!");
                return View(model);
            }

            User user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName, 
                LastName = model.LastName,
                Subjects = await HttpContext.RequestServices.GetRequiredService<ISubject>().GetAllSubjectsAsync()
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(model);
    }

    [Route("/login")]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel() { ReturnUrl = returnUrl });
    }

    [Route("/login")]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверный логин и (или) пароль");
        }

        return View(model);
    }

    [Route("/logout")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}