
using Microsoft.AspNetCore.Mvc;

public class SubscribeViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("Subscribe");
    }
}