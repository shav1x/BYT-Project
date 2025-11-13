namespace Project.Classes;

public class Staff(
    string name,
    string surname,
    DateTime dateOfBirth,
    string email,
    string? phone,
    string role,
    decimal salary)
    : Person(name, surname, dateOfBirth, email, phone)
{
    public required string Role { get; set; } = role;
    public required decimal Salary { get; set; } = salary;
}