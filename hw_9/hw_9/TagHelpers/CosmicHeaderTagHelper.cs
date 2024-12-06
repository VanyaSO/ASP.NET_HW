using Microsoft.AspNetCore.Razor.TagHelpers;

namespace hw_9.TagHelpers;

[HtmlTargetElement("cosmic-header")]
public class CosmicHeaderTagHelper : TagHelper
{
    public string Title { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "h1";
        output.Attributes.Add("class", "cosmic-header");

        output.Content.SetHtmlContent($"<span class='cosmic-text'>{Title}</span>");
    }
}
