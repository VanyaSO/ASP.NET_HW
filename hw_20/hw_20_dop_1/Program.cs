using hw_20_dop_1.Data;
using hw_20_dop_1.Interfaces;
using hw_20_dop_1.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});
builder.Services.AddScoped<ICart, CartRepository>();
builder.Services.AddScoped<IBook, BookRepository>();
builder.Services.AddScoped<IOrder, OrderRepository>();
builder.Services.AddScoped<IUser, UserRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    ApplicationContext applicationContext = services.GetService<ApplicationContext>();
    Initializer.Init(applicationContext);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();