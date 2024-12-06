using hw_9.Models;
using hw_9.ViewModels;

namespace hw_9.Services;

public class GenreService
{
    private List<Genre> _genres = new List<Genre>()
    {
        new Genre() { Id = 1, Name = "Historica" },
        new Genre() { Id = 2, Name = "Romance" },
        new Genre() { Id = 3, Name = "Dystopian" }
    };
        
    public List<Genre> GetAllGenres() => _genres.ToList();

    public Genre? GetGenreById(int id) => _genres.FirstOrDefault(g => g.Id == id);
}