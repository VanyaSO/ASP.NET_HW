using lesson_08_01_blog.Interfaces;
using lesson_08_01_blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lesson_08_01_blog.Controllers;

[Authorize(Roles = "Admin")]
public class MembershipController : Controller
{
    private readonly IMembership _membership;

    public MembershipController(IMembership membership)
    {
        _membership = membership;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var allMemberships = await _membership.GetAllMembershipsAsync();
        return View(allMemberships); // Передача даних у View
    }
    
    [HttpGet]
    public async Task<IActionResult> Generate([FromServices] IHttpContextAccessor httpContextAccessor)
    {
        string code = Guid.NewGuid().ToString();
        string link = HttpContext.Request.Scheme + "://" + httpContextAccessor.HttpContext.Request.Host.Value + "/register?code=" + code;
        var membership = new Membership
        {
            CreateDate = DateTime.Now,
            IsEnable = true,
            Code = code,
            Link = link
        };
        await _membership.AddMembershipAsync(membership);
        return RedirectToAction(nameof(Index));
    }
    
    // [HttpPost]
    // [AutoValidateAntiforgeryToken]
    // public async Task<IActionResult> Delete()

}