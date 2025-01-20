using lesson_08_01_blog.Models;

namespace lesson_08_01_blog.Interfaces;

public interface ISubscriber
{
    public Task AddSubscriberAsync(Subscriber subscriber);
    public Task<Subscriber> GetSubscriberByIdAsync(string id);
    public Task DeleteSubscriberAsync(Subscriber subscriber);
    public Task<bool> IsEmailSubscribed(string email);
    public Task<IEnumerable<Subscriber>> GetAllSubscribers();
}