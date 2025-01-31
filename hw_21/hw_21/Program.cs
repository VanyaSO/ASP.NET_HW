using EmailService;
using EmailService.Interfaces;
using hw_20_dop_1.Data;
using hw_21.AccountJob;
using hw_21.Helpers;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

// Email sender
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<LoggerInfo>();

// Quartz
builder.Services.AddQuartz(e =>
{
    var jobKey = new JobKey("AccountDelete");

    e.AddJob<AccountDelete>(opts => opts.WithIdentity(jobKey));
    e.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("AccountDelete-trigger")
        .WithCronSchedule("0 0 0,12 * * ?")
    );
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


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