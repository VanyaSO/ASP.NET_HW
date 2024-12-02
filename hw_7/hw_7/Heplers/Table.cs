using System.Reflection;
using hw_7.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
namespace hw_7.Heplers;

public static class Table
{
    public static TableViewModel Sort(IEnumerable<dynamic> items, List<string> sortBy, string value, string? option)
    {
        if (!items.Any()) 
            throw new ArgumentException("Сannot sort, list is empty");
        
        if (!sortBy.Contains(value)) 
            throw new ArgumentException("Not found value for sort");

        TableViewModel model = new TableViewModel(items);
        PropertyInfo property  = model.Type.GetProperty(value);
        if (property is null)
            throw new ArgumentException($"Property \"{value}\" not found!");
        
        if (option is not null)
            model.Items = option == "asc" 
                ? model.Items.OrderBy(e => property.GetValue(e)).ToList()
                : model.Items.OrderByDescending(e => property.GetValue(e)).ToList();

        return model;
    }
    public static TableViewModel Search(IEnumerable<dynamic> items, List<string> searchBy, string value, string option)
    {
        if (!items.Any()) 
            throw new ArgumentException("Сannot search, list is empty");
        
        if (!searchBy.Contains(option)) 
            throw new ArgumentException("Сannot search by this option");

        TableViewModel model = new TableViewModel(items);
        PropertyInfo property  = model.Type.GetProperty(option);
        if (property is null)
            throw new ArgumentException($"Property \"{option}\" not found!");

        model.Items = model.Items.Where(e => property.GetValue(e) == value).ToList();

        return model;
    }
}