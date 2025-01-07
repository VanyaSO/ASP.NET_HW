using hw_14.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hw_14;

public class ProductService
{
    private ApplicationContext _db;

    public ProductService(ApplicationContext db)
    {
        _db = db;
    }

    public IEnumerable<Product> GetAllProducts() => _db.Products.ToList();
}