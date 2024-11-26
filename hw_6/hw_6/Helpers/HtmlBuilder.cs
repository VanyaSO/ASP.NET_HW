using System.Reflection;
using System.Text;
using hw_6.Models;

namespace hw_6.Helpers;

public class HtmlBuilder
{
    public static string GetAllCourses(List<Course>? courses, string token)
    {
        StringBuilder coursesHtml = new StringBuilder();
        coursesHtml.Append("<div class=\"courses\">");

        string courseTemplate = $$"""
            <div class="course-card">
                <div>
                    <h3 class="mb-3">{0}</h3>
                    <p>{1}</p>
                    <div>Date: from {2} to {3}</div>
                </div>
                <div>
                    <form action="/courses/register" method="post">
                        <input name="courseId" value="{4}" hidden>
                        <input name="token"" value="{{token}}" hidden>
                        <button 
                            type="submit" 
                            class="btn btn-primary w-100"
                            {5}
                        </button>
                    </form>
                </div>
            </div>
            """;

        if (courses != null && courses.Any())
        {
            bool isRegistered;
            foreach (var course in courses)
            {
                isRegistered = course.Users.Any(u => u.Token == token);
                coursesHtml.AppendFormat(
                    courseTemplate,
                    course.Name,
                    course.Description,
                    course.DateStart.ToShortDateString(),
                    course.DateEnd.ToShortDateString(),
                    course.Id,
                    isRegistered ? "disabled>Registered" : ">Register"
                );
            }
        }
        else
        {
            coursesHtml.Append("Courses not found");
        }
        
        coursesHtml.Append("</div>");


        return coursesHtml.ToString();
    }

    public static string GetNavbar(string token)
    {
        return $$"""
             <nav class="navbar navbar-expand-lg navbar-light bg-light">
               <div class="container-fluid">
                 <a class="navbar-brand" href="#">Navbar</a>
                 <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                   <span class="navbar-toggler-icon"></span>
                 </button>
                 <div class="collapse navbar-collapse" id="navbarNav">
                   <ul class="navbar-nav">
                     <li class="nav-item">
                       <a class="nav-link active" aria-current="page" href="/?token={{token}}">Home</a>
                     </li>
                     <li class="nav-item">
                       <a class="nav-link" href="/courses?token={{token}}">All Courses</a>
                     </li>
                     <li class="nav-item">
                       <a class="nav-link" href="/user/courses?token={{token}}">My courses</a>
                     </li>
                     <li class="nav-item">
                       <a class="nav-link" href="/login">Log out</a>
                     </li>
                   </ul>
                 </div>
               </div>
             </nav>
             
             """;
    }
    
    public static string GetUserCourses(List<Course>? courses)
    {
        StringBuilder coursesHtml = new StringBuilder();
        coursesHtml.Append("<div class=\"courses\">");

        string courseTemplate = $$"""
              <div class="course-card flex-column">
                  <h3 class="mb-3">{0}</h3>
                  <p>{1}</p>
                  <div>Date of the event: from {2} to {3}</div>
              </div>
              """;

        if (courses != null && courses.Any())
        {
            foreach (var course in courses)
            {
                coursesHtml.AppendFormat(
                    courseTemplate,
                    course.Name,
                    course.Description,
                    course.DateStart.ToShortDateString(),
                    course.DateEnd.ToShortDateString()
                );
            }
        }
        else
        {
            coursesHtml.Append("Courses not found");
        }
        
        coursesHtml.Append("</div>");


        return coursesHtml.ToString();
    }

    public static string GetRegisterForm()
    {
        string form = $$"""
             <form action="/register" method="post" style="max-width: 440px; margin: 0 auto;">
                 <div class="mb-3">
                     <label for="fullName" class="form-label">Full name</label>
                     <input type="text" class="form-control" id="fullName" name="fullName" required>
                 </div>
                 <div class="mb-3">
                     <label for="email" class="form-label">Email</label>
                     <input type="email" class="form-control" id="email" name="email" required>
                 </div>
                 <div class="mb-3">
                     <label for="password" class="form-label">Password</label>
                     <input type="password" class="form-control" id="password" name="password" required>
                 </div>
                 <div class="mb-3">
                     <label for="phone" class="form-label">Phone</label>
                     <input type="tel" class="form-control" id="phone" name="phone" required>
                 </div>
                 <div class="my-4">
                    <a href="/login">Login</a>
                 </div>
                 <div class="text-center">
                    <button type="submit" class="btn btn-primary w-50 ">Register</button>
                 </div>
             """;
        return form;
    }

    public static string GetLogInForm()
    {
        string form = $$"""
            <form action="/login" method="post" style="max-width: 440px; margin: 0 auto;">
                <div class="mb-3">
                    <label for="email" class="form-label">Email</label>
                    <input type="email" class="form-control" id="email" name="email" required>
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Password</label>
                    <input type="password" class="form-control" id="password" name="password" required>
                </div>
                <div class="my-4">
                   <a href="/register">Register</a>
                </div>
                <div class="text-center">
                   <button type="submit" class="btn btn-primary w-50 ">Log In</button>
                </div>
            """;
        return form;
    }

    
    public static string GenerateHtmlPage(string nav, string body, string header)
    {
        string html = $$"""
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset="utf-8" />
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" 
                integrity="sha384-KK94CHFLLe+nY2dmCWGMq91rCGa5gtU4mk92HdvYe+M/SXH301p5ILy+dN9+nJOZ" crossorigin="anonymous">
                <link rel="stylesheet" href="/css/site.css"/>
                <title>{{header}}</title>
            </head>
            <body>
            <header>{{nav}}</header>
            <div class="container">
                <h2 class="d-flex justify-content-center">{{header}}</h2>
                {{body}}
            </div>
        
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
            </body>
            </html>
        """;
        return html;
    }
}