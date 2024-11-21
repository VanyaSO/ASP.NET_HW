var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.Map("/", async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(HtmlHelper.GenerateHtmlPage(HtmlHelper.BuildSlider()));
});

app.Map("/upload", async (context) =>
{
    string imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");;
    
    if (context.Request.Method == HttpMethods.Post)
    {
        var files = context.Request.Form.Files;
        if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);
        foreach (var file in files)
        {
            string fullPath = $"{imagesPath}/{file.FileName}";
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        context.Response.Redirect("/");
    }
});

app.Run();
