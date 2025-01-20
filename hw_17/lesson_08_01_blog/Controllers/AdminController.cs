using lesson_08_01_blog.Models;
using lesson_08_01_blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lesson_08_01_blog.Controllers;

public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<User> um, RoleManager<IdentityRole> rm)
    {
        _userManager = um;
        _roleManager = rm;
    }

    [Route("users")]
    [HttpGet]
    public IActionResult Index() 
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    [Route("edit-user")]
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var model = new EditUserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Phone = user.PhoneNumber
        };

        return View(model);
    }

    
    [Route("edit-user")]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Email = model.Email;
                user.Name = model.Name;
                user.PhoneNumber = model.Phone;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        return View(model);
    }
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
    
        return RedirectToAction(nameof(Index));
    }
    
    [Route("user-roles")]
    [HttpGet]
    public async Task<IActionResult> EditRoles(string userId)
    {
        // получаем пользователя
        User user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // получаем список ролей пользователя
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();
            ChangeRoleViewModel model = new ChangeRoleViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return View(model);
        }
        return NotFound();
    }
 
    [Route("user-roles")]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> EditRoles(string userId, List<string> roles)
    {
        // получаем пользователя
        User user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // получаем список ролей пользователя
            var userRoles = await _userManager.GetRolesAsync(user);
            // получаем список ролей, которые были добавлены
            var addedRoles = roles.Except(userRoles);
            // получаем роли, которые были удалены
            var removedRoles = userRoles.Except(roles);
 
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
 
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }
}