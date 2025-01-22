using System.Security.Claims;
using hw_18.Models;
using Microsoft.AspNetCore.Mvc;

namespace hw_18.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static bool IsHasAccess(this ClaimsPrincipal user, HttpContext context)
    {
        if (user != null)
        {
            var emailsConfig = context.RequestServices.GetRequiredService<EmailsConfig>();

            string? userEmail = user.FindFirst(ClaimTypes.Name)?.Value;
            return !string.IsNullOrEmpty(userEmail) && emailsConfig.Emails.Count>0 != null && emailsConfig.Emails.Contains(userEmail);
        }

        return false;
    }
}