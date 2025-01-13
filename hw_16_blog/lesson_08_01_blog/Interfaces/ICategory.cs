using lesson_08_01_blog.Models;

namespace lesson_08_01_blog.Interfaces;

public interface ICategory
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryAsync(string id);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
}