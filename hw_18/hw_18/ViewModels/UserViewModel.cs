using hw_18.Models;

namespace hw_18.ViewModels;

public class UserViewModel
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<Subject> Subjects { get; set; }
 }