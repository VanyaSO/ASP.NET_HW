using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace hw_7.Controllers;

public class TaskDop4Controller : Controller
{
    private readonly List<string> _themes = new List<string>() { "Баг", "Идея" };
    private static EmailConfiguration _emailConfiguration = new EmailConfiguration()
    {
        From = "ushachovg324@gmail.com",
        SmtpServer = "smtp.gmail.com",
        Port = 587,
        UserName = "ushachovg324@gmail.com",
        Password = "gwhv laef eljn"
    };
    
    public IActionResult Index()
    {
        return View(_themes);
    }
    
    [HttpPost]
    public async Task<IActionResult> Send(FeedbackModel feedback)
    {
        if (!ModelState.IsValid || !_themes.Contains(feedback.Theme) || !SendMessage(feedback))
        {
            return PartialView("_Message", (message: "Произошла ошибка", isError: true));
        }
        return PartialView("_Message", (message: "Спасибо за обратную связь", isError: false));
    }
    
    private bool SendMessage(FeedbackModel feedback)
    {
        try
        {
            MailMessage post = new MailMessage();
            post.From = new MailAddress(_emailConfiguration.From);
            post.To.Add(_emailConfiguration.From);
            post.Subject = $"{feedback.Email} отпривил письмо на тему {feedback.Theme} ";
            post.Body = feedback.Message;

            SmtpClient smtpClient = new SmtpClient(_emailConfiguration.SmtpServer, _emailConfiguration.Port);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_emailConfiguration.UserName, _emailConfiguration.Password);     
            smtpClient.Send(post);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
}

class EmailConfiguration
{
    public string From { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class FeedbackModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Theme { get; set; }
    [Required]
    public string Message { get; set; }
}
