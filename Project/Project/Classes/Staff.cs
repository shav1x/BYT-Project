namespace Project.Classes;

public class Staff : Person
{
    private decimal _salary;
    private string _role;
    
    public decimal Salary
    {
        get => _salary;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Salary must be positive");

            _salary = value;
        }
    }
    
    public string Role
    {
        get => _role;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Role cannot be empty");

            _role = value;
        }
    }
    
    public Staff(string name, string surname, DateTime dateOfBirth, string email, string? phone, string role, decimal salary) : base(name, surname, dateOfBirth, email, phone)
    {
        Role = role;
        Salary = salary;
    }
}