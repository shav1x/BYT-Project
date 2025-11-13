using System.Text.RegularExpressions;

namespace Project.Classes;

public abstract class Person
{
    public required string Name { get; set; }

    public required string Surname { get; set; }

    private string _email;

    public required string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Invalid email format", nameof(Email));
            }
            _email = value;
        }
    }

    public string? Phone { get; set; }

    private DateTime _dateOfBirth;

    public required DateTime BirthDate
    {
        get => _dateOfBirth;
        set
        {
            if (value > DateTime.Today)
            {
                throw new ArgumentException("Date of birth cannot be in the future", nameof(BirthDate));
            }
            _dateOfBirth = value;
        }
    }

    public int Age => DateTime.Today.Year - BirthDate.Year - (BirthDate > DateTime.Today.AddYears(-(DateTime.Today.Year - BirthDate.Year)) ? 1 : 0);

    protected Person(string name, string surname, DateTime dateOfBirth, string email, string? phone)
    {
        _email = email;
        _dateOfBirth = dateOfBirth;
        Name = name;
        Surname = surname;
        Phone = phone;
    }
}
