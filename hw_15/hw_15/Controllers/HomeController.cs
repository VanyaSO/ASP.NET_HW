using EmailService;
using EmailService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using hw_15.Models;
using hw_15.ViewModels;

namespace hw_15.Controllers;

public class HomeController : Controller
{
    private readonly IEmailSender _emailSender;
 
    public HomeController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
 
    [HttpPost]
    public IActionResult SendLetter(EmailLetterViewModel letter)
    {
        if (ModelState.IsValid)
        {
            var message = new Message(new string[] { letter.Email }, letter.Title, letter.Comment);
            _emailSender.SendEmail(message);
            return Content($"Thank you for your trust, the letter will be resent on {letter.Date}");
        }

        return BadRequest();
    }
 
    public IActionResult Index()
    {
        return View(new EmailLetterViewModel() {Title = "For the future me", Comment = "Dear FutureMe"});
    }
}
