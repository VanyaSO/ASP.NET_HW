using hw_8_dop_2.Models;
using hw_8_dop_2.ViewModels;

public class EmployeeService
{
    private List<Employee> _employees = new List<Employee>
    {
        new Employee { Id = 1, FullName = "Иван Иванов", Salary = 50000 },
        new Employee { Id = 2, FullName = "Анна Смирнова", Salary = 60000 },
        new Employee { Id = 3, FullName = "Петр Петров", Salary = 55000 },
        new Employee { Id = 4, FullName = "Ольга Сидорова", Salary = 58000 },
        new Employee { Id = 5, FullName = "Дмитрий Кузнецов", Salary = 62000 }
    };

    public void UpdateRange(List<EmployeeUpdateViewModal> employees)
    {
        foreach (var empl in employees)
        {
            _employees.FirstOrDefault(e => e.Id == empl.Id).Salary = empl.NewSalary;
        }
    }

    public List<Employee> GetAllEmployees() => _employees.ToList();
}