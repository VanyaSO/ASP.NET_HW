using hw_14.Models;

namespace hw_14;

public class CurrencyService
{
    private ApplicationContext _db;

    public CurrencyService(ApplicationContext db)
    {
        _db = db;
    }

    public Currency? GetCurrencyById(int id) => _db.Currencies.FirstOrDefault(c => c.Id == id);
    public IEnumerable<Currency> GetAllCurrencies() => _db.Currencies.ToList();
}