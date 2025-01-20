using lesson_08_01_blog.Interfaces;
using lesson_08_01_blog.Models;
using lesson_08_01_blog.Models.Pages;
using Microsoft.EntityFrameworkCore;

namespace lesson_08_01_blog.Repositories;

public class PublicationRepository : IPublication
{
    private readonly ApplicationContext _context;
 
    public PublicationRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Publication>> GetRandomPublications(int amount = 4)
    {
        return await _context.Publications
            .Include(e => e.Categories)
            .OrderBy(_ => EF.Functions.Random())
            .Take(amount)
            .ToListAsync();
    }
 
    public async Task AddPublicationAsync(Publication publication)
    {
        if (publication.Categories.Count > 0)
        {
            var categoriesId = publication.Categories.Select(e => e.Id).ToArray();
            var allCategories = _context.Categories.Where(e => categoriesId.Contains(e.Id)).ToList();
 
            publication.Categories = allCategories;
        }
 
        _context.Publications.Add(publication);
        await _context.SaveChangesAsync();
    }
 
    public async Task DeletePublicationAsync(Publication publication)
    {
        _context.Publications.Remove(publication);
        await _context.SaveChangesAsync();
    }
 
    public async Task<IEnumerable<Publication>> GetAllPublicationsAsync()
    {
        return await _context.Publications.ToListAsync();
    }
 
    public async Task<IEnumerable<Publication>> GetPublicationWithCategoriesWithUserAsync(QueryOptions options)
    {
        return new PagedList<Publication>(_context.Publications.Include(e => e.Categories).Include(e => e.User) ,options);
    }
    
    public async Task<IEnumerable<Publication>> GetAllPublicationsByCategoriesWithUserAsync(QueryOptions options, string id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(e => e.Id.ToString() == id);
        if (category is not null)
        {
            return new PagedList<Publication>(
                _context.Publications.Include(e => e.User).Include(e => e.Categories).Where(e => e.Categories.Any(c => c.Id == category.Id)), options);
        }

        return new PagedList<Publication>(_context.Publications.Include(e => e.Categories), options);
    }
 
    public async Task<Publication> GetPublicationAsync(string id)
    {
        return await _context.Publications.FirstOrDefaultAsync(e => e.Id.ToString() == id);
    }
 
    public async Task<Publication> GetPublicationWithCategoriesWithUserAsync(string id)
    {
        return await _context.Publications.Include(e => e.Categories).Include(e => e.User).FirstOrDefaultAsync(e => e.Id.ToString() == id);
    }
 
    public async Task UpdatePublicationAsync(Publication publication)
    {
        var categoriesId = publication.Categories.Select(e => e.Id).ToArray();
        var allCategories = _context.Categories.Where(e => categoriesId.Contains(e.Id)).ToList();
 
        var currentPublication = await GetPublicationWithCategoriesWithUserAsync(publication.Id.ToString());
        currentPublication.Title = publication.Title;
        currentPublication.Description = publication.Description;
        currentPublication.Categories = allCategories;
        currentPublication.SeoDescription = publication.SeoDescription;
        currentPublication.Keywords = publication.Keywords;
        currentPublication.FullImageName = publication.FullImageName;
        currentPublication.Image = publication.Image;
        currentPublication.UserId = publication.UserId;
 
        _context.Publications.Update(currentPublication);
        await _context.SaveChangesAsync();
    }
 
    public async Task UpdateViewsAsync(string id)
    {
        await _context.Database.ExecuteSqlRawAsync($"UPDATE [Publications] SET [TotalViews] += 1 WHERE Id = '{id}'");
    }
}