using hw_5.Interfaces;
using hw_5.Models;
using Microsoft.EntityFrameworkCore;

namespace hw_5.Repositories;

public class ProductRepository : IProduct
{
    private readonly ApplicationContext _context;

    public ProductRepository(ApplicationContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll() => _context.Products.ToList();

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public IEnumerable<Product> Search(string keyword) => _context.Products
        .Where(e => e.Name.ToLower().Contains(keyword.ToLower()))
        .ToList();

    public Product GetProductById(int id) => _context.Products.FirstOrDefault(e => e.Id == id);
 
    public Product Remove(int id)
    {
        var product = GetProductById(id);
        _context.Products.Remove(product);
        _context.SaveChanges();
        return product;
    }
}