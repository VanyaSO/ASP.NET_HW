using hw_20_dop_1.Data;
using hw_21.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hw_21.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ApplicationContext _context;

    public AccountController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAccount(string id, [FromServices] LoggerInfo loggerInfo)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id);
        if (user == null) return NotFound(new { message = "Account not found" });
        if (user.DeleteAt != null) return BadRequest(new { message = "Account marked for deletion" });

        loggerInfo.Log($"User Id:{user.Id} Email: {user.Email} deleted account");
        user.DeleteAt = DateTime.UtcNow.AddDays(30);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "The account will be deleted automatically after 30 days" });
    }
}