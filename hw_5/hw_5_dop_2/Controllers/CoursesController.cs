using hw_5_dop_2.Interface;
using hw_5_dop_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace hw_5_dop_2.Controllers;

public class CoursesController : Controller
{
    private readonly ICourse _courses;
    private readonly ICourseSubscription _subscriptions;
    public CoursesController(ICourse courses, ICourseSubscription subscriptions)
    {
        _courses = courses;
        _subscriptions = subscriptions;
    }
    
    public IActionResult Index()
    {
        return View(_courses.GetAll());
    }

    public IActionResult Subscribe([FromForm] int courseId, [FromForm] string email)
    {
        try
        {
            _subscriptions.AddSubscribe(new CourseSubscription(){ Email = email, CourseId = courseId});
            TempData["SuccessMessage"] = "You have successfully subscribed to the course!";
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        
        return RedirectToAction("Index");
    }
}