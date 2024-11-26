using System.ComponentModel.DataAnnotations;
using hw_6.Helpers;
using hw_6.Interfaces;
using hw_6.Models;
using hw_6.Repositories;
using hw_6.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<ICourse, CourseRepository>();
builder.Services.AddScoped<IUserCourse, UserCourseRepository>();
builder.Services.AddScoped<Database>();

IConfigurationRoot confString = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(confString.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    context.RequestServices.GetService<IUser>()?.Register(new User {FullName = "Admin", Email = "admin@gmail.com", Password = "admin", Phone = "000-000-00-00"});
    context.RequestServices.GetService<Database>()?.Init();
    await next.Invoke();
});

app.Run(async context =>
{
    HttpRequest request = context.Request;
    HttpResponse response = context.Response;
    response.ContentType = "text/html; charset=utf-8";

    string? token = request.Query["token"];
    
    var userRepository = context.RequestServices.GetService<IUser>();

    if (request.Path == "/login" && request.Method == HttpMethods.Get)
    {
        await response.WriteAsync(HtmlBuilder.GenerateHtmlPage("",HtmlBuilder.GetLogInForm(), "Login"));
    }    
    else if (request.Path == "/login" && request.Method == HttpMethods.Post)
    {
        var form = request.Form;
        string email = form["email"];
        string password = form["password"];
        
        if (email.IsNullOrEmpty() || password.IsNullOrEmpty()) response.Redirect("/login");
        
        string? tokenLogIn = userRepository?.LogIn(email, password);
        
        response.Redirect($"/?token={tokenLogIn}");
    }
    else if (request.Path == "/register" && request.Method == HttpMethods.Get)
    {
        await response.WriteAsync(HtmlBuilder.GenerateHtmlPage("",HtmlBuilder.GetRegisterForm(), "Register"));
    } 
    else if (request.Path == "/register" && request.Method == HttpMethods.Post)
    {
        var form = request.Form;
        string fullName = form["fullName"];
        string email = form["email"];
        string password = form["password"];
        string phone = form["phone"];

        if (fullName.IsNullOrEmpty() && email.IsNullOrEmpty() && password.IsNullOrEmpty() && phone.IsNullOrEmpty()) response.Redirect("/register");
        
        userRepository?.Register(new User {FullName = fullName, Email = email, Password = password, Phone = phone});
        response.Redirect("/login");
    }
    else if (request.Path == "/courses/register" && request.Method == HttpMethods.Post)
    {
        var form = request.Form;
        string tokenForm = form["token"];
        int courseId = Int32.Parse(form["courseId"]);
        
        var userCourseRepository = context.RequestServices.GetService<IUserCourse>();
        var courseRepository = context.RequestServices.GetService<ICourse>();
        
        var course = courseRepository?.GetCourseById(courseId);
        var user = userRepository?.GetUserByToken(tokenForm);
        if (user != null && course != null)
        {
            userCourseRepository?.AddRegistration(new UserCourse {UserId = user.Id, CourseId = course.Id, DateRegister = DateTime.Now});
        }

        response.Redirect($"/courses?token={tokenForm}");
    }
    else if (token.IsNullOrEmpty() || !userRepository.ValidateToken(token))
    {
        response.Redirect("/login");
    }
    else if (request.Path == "/" && request.Method == HttpMethods.Get)
    {
        await response.WriteAsync(HtmlBuilder.GenerateHtmlPage( HtmlBuilder.GetNavbar(token),"","Home page"));
    }
    else if (request.Path == "/courses" && request.Method == HttpMethods.Get)
    {
        var coursesRepository = context.RequestServices.GetService<ICourse>();

        var allCourses = coursesRepository?.GetAll();
        
        await response.WriteAsync(HtmlBuilder.GenerateHtmlPage(HtmlBuilder.GetNavbar(token),HtmlBuilder.GetAllCourses(allCourses, token), "All courses"));
    }
    else if (request.Path == "/user/courses" && request.Method == HttpMethods.Get) {
        var userCourses = userRepository?.GetCoursesForUser(token);
        
        await response.WriteAsync(HtmlBuilder.GenerateHtmlPage(HtmlBuilder.GetNavbar(token),HtmlBuilder.GetUserCourses(userCourses), "Your courses"));
    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsync("not found");
    }
});




app.Run();