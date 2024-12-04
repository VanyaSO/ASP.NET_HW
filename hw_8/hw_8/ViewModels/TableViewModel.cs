namespace hw_8.ViewModels;

public class TableViewModel
{
    public Type Type { get; set; }
    public IEnumerable<dynamic> Items { get; set; }

    public TableViewModel() {}
    public TableViewModel(IEnumerable<dynamic> items)
    {
        Type = items.GetType().GetGenericArguments()[0];
        Items = items;
    }
}