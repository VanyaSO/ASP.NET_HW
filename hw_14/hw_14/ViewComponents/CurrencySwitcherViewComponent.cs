using Microsoft.AspNetCore.Mvc;

namespace hw_14.ViewComponents;

using Microsoft.AspNetCore.Mvc;

public class CurrencySwitcherViewComponent : ViewComponent
{
    private readonly CurrencyService _currencyService;

    public CurrencySwitcherViewComponent(CurrencyService currencyService)
    {
        _currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));
    }

    public IViewComponentResult Invoke()
    {
        var model = _currencyService.GetAllCurrencies();
        return View("CurrencySwitcher", model);
    }
}
