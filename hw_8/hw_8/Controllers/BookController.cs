using hw_8.Services;
using hw_8.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace hw_8.Controllers;

public class BookController : Controller
{
    private readonly BookService _service;
    public BookController(BookService service)
    {
        _service = service;
    }
    
    public IActionResult Index()
    {
        List<BookViewModel> books = _service.GetAllBooks();
        
        return View(new TableViewModel(books));
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Add(BookViewModel book)
    {
        if (ModelState.IsValid)
        {
            _service.Add(book);
            return RedirectToAction(nameof(Index));
        }
        
        return RedirectToAction(nameof(Add));
    }
}