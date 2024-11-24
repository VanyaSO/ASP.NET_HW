using hw_5.Interfaces;
using hw_5.Models;
using Microsoft.AspNetCore.Mvc;

namespace hw_5.Controllers;

public class ProductController : Controller
{
    private readonly IProduct _products;
    public ProductController(IProduct repository)
    {
        _products = repository;
    }
    
    // Index() - метод, который будет возвращать список всех товаров на сайте в виде JSON; 
    public IActionResult Index()
    {
        var allProducts = _products.GetAll();
        return Json(allProducts);
    }
    
    
    // Create() - метод, который будет добавлять товар, через POST запрос. Для этого используйте HTML страницу или действие возвращающее соответствующую форму для заполнения данных о товаре.
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create([FromForm]Product product)
    {
        _products.Add(product);
        return RedirectToAction("Index", "Home");
    }
    
    // Search(string keyword) - метод, который будет искать товары по ключевому слову и возвращать их в виде JSON; 
    public IActionResult Search(string keyword)
    {
        var products = _products.Search(keyword);
        return Json(products);
    }
    
    // Details(int id) - метод, который будет отображать подробную информацию о товаре по его идентификатору в виде JSON;
    public IActionResult Details(int id)
    {
        var product = _products.GetProductById(id);
        return Json(product);
    }
    
    // Delete(int id) - метод, который будет удалять товар по его идентификатору, через POST запрос и в качестве ответа возвращать удаленный товар виде JSON.
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var product = _products.Remove(id);
        return Json(product);
    }
}