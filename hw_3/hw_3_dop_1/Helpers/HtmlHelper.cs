using System.Reflection;
using System.Text;

public static class HtmlHelper
{
    public static string GenerateHtmlPage(string body)
    {
        string html = $$"""
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset="utf-8" />
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" 
                integrity="sha384-KK94CHFLLe+nY2dmCWGMq91rCGa5gtU4mk92HdvYe+M/SXH301p5ILy+dN9+nJOZ" crossorigin="anonymous">
                <title>Gallery</title>
            </head>
            <body>
            <div class="container">
                {{body}}
                
                <form action="upload" method="post" enctype="multipart/form-data" class="form mt-5 text-center mx-auto" style="width: 400px;">
                    <input class="form-control" type="file" name="uploads">
                    <button class="btn btn-primary mt-3"  type="submit">Загрузить</button>
                </form>
            </div>
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
            </body>
            </html>
        """;
        return html;
    }

    public static string BuildSlider()
    {
        string imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        
        if (Directory.Exists(imagesPath))
        {
            var listImages = Directory.GetFiles(imagesPath, "*.*").ToList();
        
            StringBuilder slider = new StringBuilder();
            slider.Append("<div id=\"carouselExample\" class=\"carousel slide\">");
            slider.Append("<div class=\"carousel-inner\">");

            for (int i = 0; i < listImages.Count; i++)
            {
                
                string fileName = Path.GetFileName(listImages[i]);
                Console.WriteLine(fileName);
            
                slider.Append($"<div class=\"carousel-item {(i == 0 ? "active" : "")}\">");
                slider.Append($"<img src=\"/images/{fileName}\" class=\"d-block w-100\" alt=\"...\">");
                slider.Append("</div>");
            }
            slider.Append("</div>");

            slider.Append(
                "<button class=\"carousel-control-prev\" type=\"button\" data-bs-target=\"#carouselExample\" data-bs-slide=\"prev\">" +
                "<span class=\"carousel-control-prev-icon\" aria-hidden=\"true\"></span>" + "<span class=\"visually-hidden\">Previous</span>" +
                "</button>" +
                "<button class=\"carousel-control-next\" type=\"button\" data-bs-target=\"#carouselExample\" data-bs-slide=\"next\">" +
                "<span class=\"carousel-control-next-icon\" aria-hidden=\"true\"></span>" + "<span class=\"visually-hidden\">Next</span>" +
                "</button>");
            slider.Append("</div>");   
            
            return slider.ToString();

        }

        return @"<h3 class=""text-center"">Not found images</h3>";
    }
}