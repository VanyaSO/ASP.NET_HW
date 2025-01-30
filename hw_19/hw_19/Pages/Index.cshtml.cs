using System.Security.Claims;
using hw_19.Data;
using hw_19.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace hw_19.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public List<TodoItem> TodoItems { get; set; }

    public IndexModel(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task OnGet()
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var user = await _userManager.FindByNameAsync(userName);
        
        if (user == null)
        {
            return;
        }

        TodoItems = await _context.TodoItems
            .Where(t => t.UserId == user.Id)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPost(string? name)
    {
        if (String.IsNullOrEmpty(name))
        {
            ModelState.AddModelError("Name", "Name cannot be empty");
            return Page();
        }

        string userId = await GetUserId();
        
        var todoItem = new TodoItem()
        {
            UserId = userId,
            Name = name,
            IsComplete = false
        };

        await _context.TodoItems.AddAsync(todoItem);
        await _context.SaveChangesAsync();

        TodoItems = await _context.TodoItems
            .Where(t => t.UserId == userId)
            .ToListAsync();

        return RedirectToPage("/Index");
    }
    
    public async Task<IActionResult> OnPostDelete(string? id)
    {
        if (String.IsNullOrEmpty(id)) return NotFound();

        var todoItem = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id.ToString() == id);
        if (todoItem == null) return NotFound();
        
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        string userId = await GetUserId();
        TodoItems = await _context.TodoItems
            .Where(t => t.UserId == userId)
            .ToListAsync();
        
        return RedirectToPage("/Index");
    }
    
    public async Task<IActionResult> OnPostUpdate(string? id, string? isComplete, string? name)
    {
        if (String.IsNullOrEmpty(id) || String.IsNullOrEmpty(name)) return NotFound();

        var todoItem = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id.ToString() == id);
        if (todoItem == null) return NotFound();

        todoItem.Name = name;
        todoItem.IsComplete = !isComplete.IsNullOrEmpty() && isComplete.Equals("on");
        
        _context.TodoItems.Update(todoItem);
        await _context.SaveChangesAsync();

        string userId = await GetUserId();
        TodoItems = await _context.TodoItems
            .Where(t => t.UserId == userId)
            .ToListAsync();
        
        return RedirectToPage("/Index");
    }

    private async Task<string> GetUserId()
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var user = await _userManager.FindByNameAsync(userName);
        return user.Id;
    }
    
}
