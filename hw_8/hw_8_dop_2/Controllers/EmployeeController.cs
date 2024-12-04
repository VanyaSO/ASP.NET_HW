using hw_8_dop_2.Models;
using hw_8_dop_2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace hw_8_dop_2.Controllers;

public class EmployeeController : Controller
{
    private readonly EmployeeService _service;
    public EmployeeController([FromServices] EmployeeService service)
    {
        _service = service;
    }
    
    public IActionResult Index()
    {
        return View(_service.GetAllEmployees());
    }

    [HttpPost]
    public IActionResult Index(List<EmployeeUpdateViewModal> employees)
    {
        List<EmployeeUpdateViewModal> updatedEmployees = employees.Where(e => e.Salary != e.NewSalary).ToList();

        if (updatedEmployees.Any())
        {
            _service.UpdateRange(updatedEmployees);
        }
        
        return RedirectToAction(nameof(Index));
    }
}