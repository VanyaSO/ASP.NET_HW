using hw_1.Model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Person> _personList = new List<Person>();

app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    if (context.Request.Path == "/")
    {
        await context.Response.WriteAsync(BuilderPage($@"
            <div class=""px-4 py-5 my-5 text-center"">
                <h1 class=""display-5 fw-bold"">Get an invitation</h1>
                <div class=""col-lg-6 mx-auto"">
                    <p class=""lead mb-4"">Fill out the form to invite to a meeting</p>
                    <div class=""d-grid gap-2 d-sm-flex justify-content-sm-center"">
                        <a href=""form"" type=""button"" class=""btn btn-primary btn-lg px-4"">Fill out the form</a>
                    </div>
                </div>
            </div>
        "));
    }
    else if (context.Request.Path == "/form")
    {
        await context.Response.WriteAsync(BuilderPage($@"
            <form method=""post"" action=""sendform"" class=""w-25 d-grid gap-3 mt-5 mx-auto"">
                <h1 class=""h3 mb-3 fw-normal"">Enter data</h1>
                <div class=""form-floating"">
                    <input name=""email"" type=""email"" class=""form-control"" id=""email"" placeholder=""name@example.com"" required>
                    <label for=""email"">Email address</label>
                </div>
                <div class=""form-floating"">
                    <input name=""fullName"" type=""text"" class=""form-control"" id=""fullName"" placeholder=""Full name"" required>
                    <label for=""fullName"">Full name</label>
                </div>
                <div class=""form-floating"">
                    <input name=""phone"" type=""tel"" class=""form-control"" id=""phone"" placeholder=""Phone"" required>
                    <label for=""phone"">Phone</label>
                </div>
                <button class=""w-100 btn btn-lg btn-primary"" type=""submit"">Send</button>
            </form>
    "));
    }
    else if (context.Request.Path == "/sendform" && context.Request.Method == "POST")
    {
        var formData = context.Request.Form;
    
        string? email = formData["email"];
        string? fullName = formData["fullName"];
        string? phone = formData["phone"];
    
        _personList.Add(new Person(email, fullName, phone));

        await context.Response.WriteAsync(BuilderPage($@"
                <div class=""d-flex flex-column align-items-center mt-5"">
                    <h1 class=""h1 mb-3 fw-normal"">Thank you!!!</h1>
                    <p class=""fs-5 mb-4"">We will send an invitation by email</p>
                    <a href=""/"" type=""button"" class=""btn btn-success btn-lg px-4"">Return to home</a>
                </div>
        "));
    }
});

app.Run();

string BuilderPage(string content)
{
    return $@"
        <!DOCTYPE html>
        <html lang=""ru"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Приглашение</title>
            <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"" rel=""stylesheet"">
        </head>
        <body>
            {content}
        </body>
        </html>";
}

