using Microsoft.AspNetCore.Identity;

namespace hw_19.Models;

public class User : IdentityUser
{
    public ICollection<TodoItem> TodoItems { get; set; }
}