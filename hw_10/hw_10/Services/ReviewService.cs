using hw_10.Models;
using Microsoft.EntityFrameworkCore;

namespace hw_10.Services;

public class ReviewService
{
    private readonly ApplicationContext _context;
    
    public ReviewService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddReviewAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }
}