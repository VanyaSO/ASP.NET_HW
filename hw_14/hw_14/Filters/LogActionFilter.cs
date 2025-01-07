using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace hw_14.Filters;

public class LogActionFilter : Attribute, IActionFilter
{
    public string? UserEmail { get; set; }
    public string Description { get; set; }

    private string FolderName = "Logs";
    private string FileName = "LogActions.txt";

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        string folderPath = Path.Combine(wwwrootPath, FolderName);
        string filePath = Path.Combine(folderPath, FileName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }

        File.AppendAllText(filePath, $"DateTime: {DateTime.Now.ToLocalTime()} | IP: {context.HttpContext.Connection.RemoteIpAddress?.ToString()} | User: {context.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? UserEmail} | Description: {Description} \n");
    }

}