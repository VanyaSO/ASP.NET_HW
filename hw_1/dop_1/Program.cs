using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Run(async (context) =>
{
    // Доп 1
    // Реализовать обработку POST запроса с возвращением ответа. Когда вы запустите приложение и сделаете POST-запрос к URL "/api/greeting" с телом запроса, содержащим строку (например, {"name": "John"}),
    // вы должны получить ответ с персонализированным приветствием (например, "Hello, John!").
    if (context.Request.Path == "/api/greeting" && context.Request.Method == "POST")
    {
        using (var reader = new StreamReader(context.Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();
            dynamic resultObject = JsonConvert.DeserializeObject(requestBody);
            
            await context.Response.WriteAsync($"Hello {resultObject?.name}");
        }
    }
    else if (context.Request.Path == "/sendData")
    {
        // Доп 2
        // Используя JavaScript или любой другой Фреймворк или PostMan, отправьте JSON объект на любое действие в C#. После получение, отобразите это значение на новой странице.
        using (var reader = new StreamReader(context.Request.Body))
        {
            var requestBody = await reader.ReadToEndAsync();
            dynamic resultObject = JsonConvert.DeserializeObject(requestBody);
            
            context.Response.Redirect($"/showData?data={resultObject?.data}");
        }
    } 
    else if (context.Request.Path == "/showData")
    {
        string? data = context.Request.Query["data"];

        await context.Response.WriteAsync(data);
    }
});

app.Run();