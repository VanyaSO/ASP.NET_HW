using EmailService;
using EmailService.Interfaces;
using lesson_08_01_blog.Helpers;
using lesson_08_01_blog.Interfaces;
using lesson_08_01_blog.Models;
using lesson_08_01_blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lesson_08_01_blog.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> um, SignInManager<User> sm)
    {
        _userManager = um;
        _signInManager = sm;
    }

    [Route("login")]
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [Route("login")]
    [HttpPost]
    [ValidateAntiForgeryToken]
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

            ModelState.AddModelError("", "Неверный логин или пароль");
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [Route("register")]
    [HttpGet]
    public async Task<IActionResult> Register([FromServices] IMembership membership, string? code)
    {
        if (!User.Identity.IsAuthenticated || code != null)
        {
            if (await membership.ExistsMembershipByCodeAsync(code))
            {
                if (await membership.EnableCodeMembershipByCodeAsync(code))
                {
                    return View(new RegisterUserViewModel() { Code = code });
                }
            }
        }

        return RedirectToAction("Index", "Home");
    }
    
    [Route("register")]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Register([FromServices] IMembership membership, RegisterUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userCheck = await _userManager.FindByEmailAsync(model.Email);
            if (userCheck != null)
            {
                ModelState.AddModelError("Email", "Такой email адрес уже есть!");
                return View(model);
            }
            User user = new User { Email = model.Email, UserName = model.Email, Name = model.Name, PhoneNumber = model.Phone };
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //Установки роли. Сама роль находится в таблице AspNetRoles
                //если таблица пустая, получим ошибку. Обязательно заполняем роли!
                await _userManager.AddToRoleAsync(user, "Editor");
                //используем приглашение
                await membership.DisableMembershipCodeAsync(model.Code);
                //установка куки
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }

    [Route("change-password")]
    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }
    
    [Route("change-password")]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        User? user = await _userManager.GetUserAsync(HttpContext.User);

        if (user is null)
        {
            return RedirectToAction("Login");
        }

        bool isPasswordTrue = await _userManager.CheckPasswordAsync(user, model.OldPassword);
        if (isPasswordTrue)
        {
            await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            await _userManager.UpdateAsync(user);
        }
        else
        {
            ModelState.AddModelError("", "Неверный пароль");
            return RedirectToAction("ChangePassword");
        }
        
        return RedirectToAction("Index", "Home");
    }
    
    [Route("reset-password")]
    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }
    
    [Route("reset-password")]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ResetPassword(EmailViewModel model, [FromServices]IEmailSender _emailSender)
    {
        if (string.IsNullOrEmpty(model.Email))
        {
            ModelState.AddModelError("", "Enter email");
            return View();
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            ModelState.AddModelError("", "Email is not registered");
            return View();
        }
        
        string newPassword = PasswordHelper.GeneratePassword();
        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

        if (result.Succeeded)
        {
            var message = new Message(new string[] { "ushachovg324@gmail.com" }, "New Password", $"Your new password: {newPassword} {HttpContext.GetUrl()}/login ");
            _emailSender.SendEmail(message);
        
            return Content("We have sent you a new password");   
        }

        return NotFound();
    }
}