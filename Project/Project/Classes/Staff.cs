namespace Project.Classes;

public class Staff : Person
{
    public required string Role { get; set; }
    public required decimal Salary { get; set; }
    
    public Staff(string name, string surname, DateTime dateOfBirth, string email, string? phone, string role, decimal salary) : base(name, surname, dateOfBirth, email, phone)
    {
        Role = role;
        Salary = salary;
    }
}