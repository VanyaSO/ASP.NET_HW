using System.Security.Claims;
using hw_14.Filters;
using hw_14.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace hw_14.Controllers;

public class CurrencyController : Controller
{
    [ServiceFilter(typeof(CurrencyResourceFilter))]
    [LogActionFilter(Description = "user switched currency")]
    public async Task<IActionResult> ChangeCurrency(string currencyId, string? returnUrl = null)
    {
        var identity = (ClaimsIdentity)User.Identity;
        identity.RemoveClaim(identity.FindFirst("currencyId"));
        identity.AddClaim(new Claim("currencyId", currencyId));
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        
        ViewBag.ActiveId = currencyId;
        return Redirect(returnUrl ?? "/");
    }
}