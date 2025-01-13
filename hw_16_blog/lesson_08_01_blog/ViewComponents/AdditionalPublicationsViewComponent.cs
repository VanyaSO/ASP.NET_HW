using lesson_08_01_blog.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lesson_08_01_blog.ViewComponents;

public class AdditionalPublicationsViewComponent : ViewComponent
{
    private readonly IPublication _publication;

    public AdditionalPublicationsViewComponent(IPublication publication)
    {
        _publication = publication;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var publications = await _publication.GetRandomPublications();
        
        return await Task.FromResult((IViewComponentResult)View("AdditionalPublications", publications));
    }
}