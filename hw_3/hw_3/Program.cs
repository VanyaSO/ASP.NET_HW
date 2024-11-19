using hw_3.Helpers;
using hw_3.Middleware;
using hw_3.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    InitDatabase.Init(context);
}

app.UseMiddleware<FreeRoutingMiddleware>();
app.UseMiddleware<TokenMiddleware>();

app.MapControllers();

app.Run();