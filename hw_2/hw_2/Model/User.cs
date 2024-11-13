namespace hw_2.Model;

record User(int Id, string Name, int Age)
{
    public User(string Name, int Age) : this(0, Name, Age)
    { }
}