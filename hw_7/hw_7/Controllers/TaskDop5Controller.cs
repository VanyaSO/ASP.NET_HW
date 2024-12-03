using hw_7.Heplers;
using Microsoft.AspNetCore.Mvc;

namespace hw_7.Controllers;

public class TaskDop5Controller : Controller
{
    private List<User> _users = new List<User>();

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
        Type = items.GetType().GetGenericArguments()[0];
        Items = items;
        SortBy = sortBy ?? new List<string>();
        SearchBy = searchBy ?? new List<string>();
    }
}