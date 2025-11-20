using System.Text.RegularExpressions;

namespace Project.Classes;

public abstract class Person
{
    private string _name = null!;
    private string _surname = null!;
    private string _email = null!;
    private string? _phone;
    private DateTime _birthDate;

    public int Age =>
        DateTime.Today.Year - BirthDate.Year -
        (BirthDate > DateTime.Today.AddYears(-(DateTime.Today.Year - BirthDate.Year)) ? 1 : 0);

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[A-Za-z]+$"))
            {
                throw new ArgumentException("Name must contain only letters.");
            }
            _name = value;
        }
    }

    public string Surname
    {
        get => _surname;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[A-Za-z]+$"))
            {
                throw new ArgumentException("Surname must contain only letters.");
            }
            _surname = value;
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Invalid email format.");
            }
            _email = value;
        }
    }

    public string? Phone
    {
        get => _phone;
        set
        {
            if (value != null && !Regex.IsMatch(value, @"^[0-9]+$"))
            {
                throw new ArgumentException("Phone must contain only numbers.");
            }
            _phone = value;
        }
    }

    public DateTime BirthDate
    {
        get => _birthDate;
        set
        {
            if (value > DateTime.Today)
                throw new ArgumentException("Birth date cannot be in the future.");

            _birthDate = value;
        }
    }

    // For JSON serialization
    protected Person() { }

    protected Person(string name, string surname, DateTime birthDate, string email, string? phone)
    {
        Name = name;
        Surname = surname;
        BirthDate = birthDate;
        Email = email;
        Phone = phone;
    }
}
