using lesson_08_01_blog.Interfaces;
using lesson_08_01_blog.Models;
using Microsoft.EntityFrameworkCore;

namespace lesson_08_01_blog.Repositories;

public class SubscriberRepository : ISubscriber
{
    private readonly ApplicationContext _context;
    public SubscriberRepository(ApplicationContext  context)
    {
        _context = context;
    }
    
    public async Task AddSubscriberAsync(Subscriber subscriber)
    {
        await _context.Subscribers.AddAsync(subscriber);
        await _context.SaveChangesAsync();
    }

    public async Task<Subscriber?> GetSubscriberByIdAsync(string id) =>
        await _context.Subscribers.FirstOrDefaultAsync(e => e.Id.ToString().Equals(id));
    
    public async Task DeleteSubscriberAsync(Subscriber subscriber)
    {
        _context.Subscribers.Remove(subscriber);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsEmailSubscribed(string email) =>
        await _context.Subscribers.AnyAsync(e => e.Email == email);

    public async Task<IEnumerable<Subscriber>> GetAllSubscribers() =>
        await _context.Subscribers.ToListAsync();
}