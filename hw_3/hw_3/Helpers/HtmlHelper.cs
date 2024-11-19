using System.Reflection;
using System.Text;

public static class HtmlHelper
{
    public static string BuildHtmlTable<T>(IEnumerable<T> collection)
    {
        StringBuilder tableHtml = new StringBuilder();
        tableHtml.Append("<table class=\"table\">");

        PropertyInfo[] properties = typeof(T).GetProperties();

        tableHtml.Append("<tr>");
        foreach (PropertyInfo property in properties)
        {
            tableHtml.Append($"<th>{property.Name}</th>");
        }
        tableHtml.Append("</tr>");

        foreach (T item in collection)
        {
            tableHtml.Append("<tr>");
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(item);
                tableHtml.Append($@"<td>{value}</td>");
            }
            tableHtml.Append("</tr>");
        }

        tableHtml.Append("</table>");
        return tableHtml.ToString();
    }


    public static string GenerateHtmlPage(string body, string header)
    {
        string html = $$"""
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset="utf-8" />
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" 
                integrity="sha384-KK94CHFLLe+nY2dmCWGMq91rCGa5gtU4mk92HdvYe+M/SXH301p5ILy+dN9+nJOZ" crossorigin="anonymous">
                <title>{{header}}</title>
            </head>
            <body>
            <div class="container">
                <h1 class="mb-4">{{header}}</h1>
                {{body}}
            </div>
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
            </body>
            </html>
        """;
        return html;
    }
}