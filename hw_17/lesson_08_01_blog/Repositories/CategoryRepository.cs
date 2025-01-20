using lesson_08_01_blog.Interfaces;
using lesson_08_01_blog.Models;
using Microsoft.EntityFrameworkCore;

namespace lesson_08_01_blog.Repositories;

public class CategoryRepository : ICategory
{
    private readonly ApplicationContext _context;
 
    public CategoryRepository(ApplicationContext context)
    {
        _context = context;
    }
 
    public async Task AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }
 
    public async Task DeleteCategoryAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
 
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
 
    public async Task<Category> GetCategoryAsync(string id)
    {
        return await _context.Categories.FirstOrDefaultAsync(e => e.Id.ToString() == id);
    }
 
    public async Task UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }
}