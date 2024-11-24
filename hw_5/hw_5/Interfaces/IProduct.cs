using hw_5.Models;

namespace hw_5.Interfaces;

public interface IProduct
{
    public IEnumerable<Product> GetAll();
    public void Add(Product product);
    public IEnumerable<Product> Search(string keyword);
    public Product GetProductById(int id);
    public Product Remove(int id);
}