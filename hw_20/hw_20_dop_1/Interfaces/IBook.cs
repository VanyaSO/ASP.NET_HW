namespace hw_20_dop_1.Interfaces;

public interface IBook
{
    public Task<IEnumerable<Book>> GetBooksWithFullInfoAsync();
    public Task<Book?> GetBookWithFullInfoByIdAsync(string id);
    public Task<IEnumerable<Book>> SearchBookAsync(string propertyName, string searchTerm);
    public Task<bool> BookExistsAsync(string id);
}