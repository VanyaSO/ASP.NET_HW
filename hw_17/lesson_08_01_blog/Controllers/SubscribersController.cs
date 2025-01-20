using EmailService;
using EmailService.Interfaces;
using lesson_08_01_blog.Interfaces;
using lesson_08_01_blog.Models;
using lesson_08_01_blog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace lesson_08_01_blog.Controllers;

public class SubscribersController : Controller
{
    private readonly ISubscriber _subscription;
    private readonly IEmailSender _emailSender;
 
    public SubscribersController(ISubscriber subscription, IEmailSender emailSender)
    {
        _subscription = subscription;
        _emailSender = emailSender;
    }
    
    public async Task<IActionResult> Index()
    {
        ViewBag.Subscribers = await _subscription.GetAllSubscribers();
        return View(new EmailNewsletterViewModel());
    }
    
    [HttpPost]
    [Route("subscribe")]
    public async Task<IActionResult> Subscribe(string email)
    {
        if (!string.IsNullOrEmpty(email.Trim()))
        {
            if (!await _subscription.IsEmailSubscribed(email))
            {
                await _subscription.AddSubscriberAsync(new Subscriber() { Email = email });
                return Content("Subscription successfully completed");
            }
            
            return Content("Email is already subscribed");   
        }

        return Content("Email cannot be empty");
    }
    
    [HttpPost]
    [Route("delete-subscriber")]
    public async Task<IActionResult> DeleteSubscriber(string id)
    {
        if (!string.IsNullOrEmpty(id.Trim()))
        {
            Subscriber sub = await _subscription.GetSubscriberByIdAsync(id);
            if (sub != null)
            {
                await _subscription.DeleteSubscriberAsync(sub);

                return Ok();
            }
        }

        return BadRequest();
    }
    
    [HttpPost]
    [Route("email-newsletter")]
    public async Task<IActionResult> EmailNewsletter(EmailNewsletterViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Emails.Count != 0)
            {
                foreach (var email in model.Emails)
                {
                    var message = new Message(new string[] { email }, model.Title, model.Content);
                    _emailSender.SendEmail(message);
                }

                return RedirectToAction(nameof(Index));
            }
        }

        return BadRequest();
    }
}