using hw_14.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hw_14.Controllers;

[Authorize]
public class ShopController : Controller
{
    public IActionResult Index([FromServices] CurrencyService currencies, [FromServices] ProductService products)
    {
        int currencyId = Int32.Parse(HttpContext.User.FindFirst("currencyId").Value);
        var currency = currencies.GetCurrencyById(currencyId);

        if (currency is null) return NotFound("Currency not found");
        
        ViewBag.Rate = currency.Rate;
        ViewBag.Symbol = currency.Symbol;
        return View(products.GetAllProducts());
    }
}