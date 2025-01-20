using lesson_08_01_blog.Models;

namespace lesson_08_01_blog.Interfaces;

public interface IPublication
{
    Task<IEnumerable<Publication>> GetRandomPublications(int amount = 4);
    Task<IEnumerable<Publication>> GetAllPublicationsAsync();
    Task<IEnumerable<Publication>> GetPublicationWithCategoriesWithUserAsync(QueryOptions options);
    Task<IEnumerable<Publication>> GetAllPublicationsByCategoriesWithUserAsync(QueryOptions options, string? id);
    Task<Publication> GetPublicationAsync(string id);
    Task<Publication> GetPublicationWithCategoriesWithUserAsync(string id);
    
    
    Task AddPublicationAsync(Publication publication);
    Task DeletePublicationAsync(Publication publication);
    Task UpdatePublicationAsync(Publication publication);
    Task UpdateViewsAsync(string id);
}