using System.Reflection;
using System.Text;

namespace hw_4.Helpers;

public static class BuildHtml
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
        tableHtml.Append($"<th>Actions</th>");

        tableHtml.Append("</tr>");

        foreach (T item in collection)
        {
            tableHtml.Append("<tr>");
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(item);
                tableHtml.Append($@"<td class=""user-{property.Name.ToLower()}"">{value}</td>");
            }

            var itemId = item.GetType().GetProperty("Id").GetValue(item);
            tableHtml.Append($@"
            <td>
                <button class=""btn btn-warning btn-sm js--update-product"" type=""button"">
                    <svg width=""16"" height=""16"" viewBox=""0 0 16 16"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                        <path d=""M12.1465 0.146447C12.3417 -0.0488155 12.6583 -0.0488155 12.8536 0.146447L15.8536 3.14645C16.0488 3.34171 16.0488 3.65829 15.8536 3.85355L5.85357 13.8536C5.80569 13.9014 5.74858 13.9391 5.68571 13.9642L0.68571 15.9642C0.500001 16.0385 0.287892 15.995 0.146461 15.8536C0.00502989 15.7121 -0.0385071 15.5 0.0357762 15.3143L2.03578 10.3143C2.06092 10.2514 2.09858 10.1943 2.14646 10.1464L12.1465 0.146447ZM11.2071 2.5L13.5 4.79289L14.7929 3.5L12.5 1.20711L11.2071 2.5ZM12.7929 5.5L10.5 3.20711L4.00001 9.70711V10H4.50001C4.77616 10 5.00001 10.2239 5.00001 10.5V11H5.50001C5.77616 11 6.00001 11.2239 6.00001 11.5V12H6.29291L12.7929 5.5ZM3.03167 10.6755L2.92614 10.781L1.39754 14.6025L5.21903 13.0739L5.32456 12.9683C5.13496 12.8973 5.00001 12.7144 5.00001 12.5V12H4.50001C4.22387 12 4.00001 11.7761 4.00001 11.5V11H3.50001C3.28561 11 3.10272 10.865 3.03167 10.6755Z"" fill=""white""/>
                    </svg>
                </button>
                <a href=""/delete?id={itemId}"" class=""btn btn-danger btn-sm align-self-end"">
                    <svg width=""16"" height=""16"" viewBox=""0 0 16 16"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                        <path d=""M5.5 5.5C5.77614 5.5 6 5.72386 6 6V12C6 12.2761 5.77614 12.5 5.5 12.5C5.22386 12.5 5 12.2761 5 12V6C5 5.72386 5.22386 5.5 5.5 5.5Z"" fill=""white""/>
                        <path d=""M8 5.5C8.27614 5.5 8.5 5.72386 8.5 6V12C8.5 12.2761 8.27614 12.5 8 12.5C7.72386 12.5 7.5 12.2761 7.5 12V6C7.5 5.72386 7.72386 5.5 8 5.5Z"" fill=""white""/>
                        <path d=""M11 6C11 5.72386 10.7761 5.5 10.5 5.5C10.2239 5.5 10 5.72386 10 6V12C10 12.2761 10.2239 12.5 10.5 12.5C10.7761 12.5 11 12.2761 11 12V6Z"" fill=""white""/>
                        <path d=""M14.5 3C14.5 3.55228 14.0523 4 13.5 4H13V13C13 14.1046 12.1046 15 11 15H5C3.89543 15 3 14.1046 3 13V4H2.5C1.94772 4 1.5 3.55228 1.5 3V2C1.5 1.44772 1.94772 1 2.5 1H6C6 0.447715 6.44772 0 7 0H9C9.55229 0 10 0.447715 10 1H13.5C14.0523 1 14.5 1.44772 14.5 2V3ZM4.11803 4L4 4.05902V13C4 13.5523 4.44772 14 5 14H11C11.5523 14 12 13.5523 12 13V4.05902L11.882 4H4.11803ZM2.5 3H13.5V2H2.5V3Z"" fill=""white""/>
                    </svg>
                </a>
            </td>");

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
            <h2 class="d-flex justify-content-center">{{header}}</h2>
            <form action="/init" method="post" class="mt-3 mb-4">
                <button type="submit" class="btn btn-primary" title="Add user">
                    Add default users
                </button>
            </form>
            <form action="/update" method="post" class="mt-5 mb-3" style="display: flex;align-items: end;gap: 20px;">
                <input type="hidden" name="id" value="0">
                <div class="form-group">
                    <label for="name">Name:</label>
                    <input type="text" class="form-control" id="update-form__name" name="name" required>
                </div>
                <div class="form-group">
                    <label for="age">Age:</label>
                    <input type="number" class="form-control" id="update-form__age" name="age" required>
                </div>
                <button type="submit" class="btn btn-success p-0" title="Add user">
                    <svg width="32" height="32" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M8 4C8.27614 4 8.5 4.22386 8.5 4.5V7.5H11.5C11.7761 7.5 12 7.72386 12 8C12 8.27614 11.7761 8.5 11.5 8.5H8.5V11.5C8.5 11.7761 8.27614 12 8 12C7.72386 12 7.5 11.7761 7.5 11.5V8.5H4.5C4.22386 8.5 4 8.27614 4 8C4 7.72386 4.22386 7.5 4.5 7.5H7.5V4.5C7.5 4.22386 7.72386 4 8 4Z" fill="white"/>
                    </svg>
                </button>
            </form>
            <form action="/search" method="get" class="mt-3 mb-3" style="display: flex;align-items: end;gap: 20px;">
                <div class="form-group">
                    <label for="optionSearch">Search by:</label>
                    <select name="option" class="form-control" id="optionSearch">
                        <option value="Name">Name</option>
                        <option value="Age">Age</option>
                    </select>
                </div>
                <div class="form-group">
                    <input type="text" class="form-control" id="valueSearch" name="value" placeholder="Enter value" required>
                </div>
                <button type="submit" class="btn btn-primary">Search</button>
                <a href="/" class="btn btn-secondary">Reset</a>
            </form>
            {{body}}
            <form name="editForm" action="/update" method="post" class="mt-4" style="display: none;align-items: end;gap: 20px;">
                <input type="hidden" name="id" id="edit-form__id">
                <div class="form-group">
                    <label for="update-form__name">Name:</label>
                    <input type="text" class="form-control" id="edit-form__name" name="name">
                </div>
                <div class="form-group">
                    <label for="edit-form__age">Age:</label>
                    <input type="number" class="form-control" id="edit-form__age" name="age">
                </div>
                <button type="submit" class="btn btn-primary">Update</button>
                <button type="button" class="btn btn-secondary js--hide-edit-form">Close</button>
            </form>
            </div>
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
            <script>
                const editForm = document.getElementsByName('editForm')[0];
                document.querySelectorAll('.js--update-product').forEach(btn => {
                    btn.addEventListener('click', function() {
                        const tr = this.closest('tr');
                        const userId = tr.querySelector('.user-id').textContent.trim();
                        const userName = tr.querySelector('.user-name').textContent.trim();
                        const userAge = tr.querySelector('.user-age').textContent.trim();
                
                        document.getElementById('edit-form__id').value = userId;
                        document.getElementById('edit-form__name').value = userName;
                        document.getElementById('edit-form__age').value = userAge;
                
                        editForm.style.display = 'flex';
                    });
                });
                
                document.querySelectorAll('.js--hide-edit-form').forEach(btn => {
                    btn.addEventListener('click', function() {
                        editForm.style.display = 'none';
                    });
                });

            </script>
            </body>
            </html>
        """;
        return html;
    }
}