namespace hw_1.Model;

public class Person
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }

    public Person(string email, string fullName, string phone)
    {
        Id = new Guid();
        Email = email;
        FullName = fullName;
        Phone = phone;
    }
}