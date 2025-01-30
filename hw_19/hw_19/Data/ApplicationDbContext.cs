using hw_19.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hw_19.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<TodoItem> TodoItems { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}