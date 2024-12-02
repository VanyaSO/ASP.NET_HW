using hw_7.Heplers;
using Microsoft.AspNetCore.Mvc;

namespace hw_7.Controllers;

public class TaskDop5Controller : Controller
{
    private List<User> _users = new List<User>
    {
        new User { Name = "Alice", Age = 30, Position = "Manager", Salary = 70000 },
        new User { Name = "Bob", Age = 25, Position = "Developer", Salary = 60000 },
        new User { Name = "Charlie", Age = 28, Position = "Designer", Salary = 55000 },
        new User { Name = "Diana", Age = 35, Position = "Manager", Salary = 80000 },
        new User { Name = "Ethan", Age = 23, Position = "Developer", Salary = 50000 },
        new User { Name = "Fiona", Age = 29, Position = "HR", Salary = 45000 },
        new User { Name = "George", Age = 40, Position = "Director", Salary = 80000 },
        new User { Name = "Hannah", Age = 27, Position = "Tester", Salary = 48000 },
        new User { Name = "Ian", Age = 32, Position = "Support", Salary = 40000 },
        new User { Name = "Jane", Age = 26, Position = "Developer", Salary = 58000 }
    };

    private List<string> _sortBy = new List<string>() { "Age", "Salary" };
    private List<string> _searchBy = new List<string> { "Name", "Position" };
    
    
    public IActionResult Index()
    {
        return View(new TableViewModel(_users, _sortBy, _searchBy));
    }

    public async Task<IActionResult> Sort(string value, string? option)
    {
        try
        {
            TableViewModel model = Table.Sort(_users, _sortBy, value, option);
            return PartialView("Table/_TableBody", model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    public async Task<IActionResult> Search(string value, string option)
    {
        try
        {
            TableViewModel model = Table.Search(_users, _searchBy, value, option);
            return PartialView("Table/_TableBody", model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Position { get; set; }
    public int Salary { get; set; }
}

public class TableViewModel
{
    public Type Type { get; set; }
    public IEnumerable<dynamic> Items { get; set; }
    public IEnumerable<string>? SortBy { get; set; }
    public IEnumerable<string>? SearchBy { get; set; }

    public TableViewModel() { }
    
    public TableViewModel(IEnumerable<dynamic> items, IEnumerable<string>? sortBy = null, IEnumerable<string>? searchBy = null)
    {
        Type = items.FirstOrDefault()?.GetType();
        Items = items;
        SortBy = sortBy ?? new List<string>();
        SearchBy = searchBy ?? new List<string>();
    }
}