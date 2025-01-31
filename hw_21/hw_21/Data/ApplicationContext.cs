using hw_21.Models;
using Microsoft.EntityFrameworkCore;

namespace hw_20_dop_1.Data;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { }
}