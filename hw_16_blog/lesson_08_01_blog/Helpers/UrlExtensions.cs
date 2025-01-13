namespace lesson_08_01_blog.Helpers;

public static class UrlExtensions
{
    public static string PathAndQuery(this HttpRequest request)
    {
        return request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
    }
    
    public static string HostAndPath(this HttpRequest request)
    {
        return $"{request.Host}{request.Path}";
    }

    public static string GetUrl(this HttpContext context)
    {
        return $"{context.Request.Scheme}://{context.Request.Host}"; 
    }
}