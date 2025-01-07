using System.Security.Claims;
using hw_14.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace hw_14.Filters;

public class CurrencyResourceFilter : Attribute, IResourceFilter
{
    private readonly CurrencyService _currencies;
    private readonly UserService _users;
    public CurrencyResourceFilter(CurrencyService currencies, UserService users)
    {
        _currencies = currencies;
        _users = users;
    }
    
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        string currencyQueryId = context.HttpContext.Request.Query["currencyId"];
        if (!currencyQueryId.IsNullOrEmpty() && Int32.TryParse(currencyQueryId, out int currencyId))
        {
            Currency? curr = _currencies.GetCurrencyById(currencyId);
            if (curr is not null)
            {
                context.HttpContext.Items["CurrencyId"] = curr;

                string? userEmail = context.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                User user = _users.GetUserByEmail(userEmail);
                user.ExchangeRateId = curr.Id;
                _users.UpdateUser(user);
            }
        }
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }
}