using EmailService;
using EmailService.Interfaces;
using hw_20_dop_1.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace hw_21.AccountJob;

[DisallowConcurrentExecution]
public class AccountDelete : IJob
{
    private readonly ApplicationContext _context;
    private readonly IEmailSender _emailSender;
    
    public AccountDelete(ApplicationContext context, IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var usersForDelete = _context.Users.Where(u => u.DeleteAt <= DateTime.UtcNow.AddDays(-30)).ToList();
        var usersForSendMessage = _context.Users
            .Where(u => u.DeleteAt <= DateTime.UtcNow.AddDays(7) && u.DeleteAt >= DateTime.UtcNow)
            .Select(u => u.Email)
            .ToList();

        if (usersForDelete.Count > 0)
        {
            _context.Users.RemoveRange(usersForDelete);
            _context.SaveChanges();
            SendMessages(usersForDelete.Select(u => u.Email).ToList(), "Account was deleted", "Your account was deleted.");
        }

        if (usersForSendMessage.Count > 0)
        {
            SendMessages(usersForSendMessage, "Account will be deleted in 7 days", "Your account will be deleted in 7 days.");
        }

        return Task.CompletedTask;
    }

    private void SendMessages(List<string> emails, string title, string content)
    {
        foreach (var email in emails)
        {
            var message = new Message(new string[] { email }, title, content);
            _emailSender.SendEmail(message);
        }
    }
}