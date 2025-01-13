namespace lesson_08_01_blog.Models;

public class QueryOptions
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    public string OrderPropertyName { get; set; }
    public string OrderName
    {
        get
        {
            return OrderPropertyName switch
            {
                "Name" => "Название",
                "TotalViews" => "Просмотры",
                "CreatedAt" => "Дата публикации",
                "" => "Не выбрано",
                _ => ""
            };
        }
    }
    public bool DescendingOrder { get; set; }
    public string SearchPropertyName { get; set; } = "Title";
    public string SearchTerm { get; set; }
}