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
    public required DateTime BirthDate { get; set; }
    public int Age => DateTime.Today.Year - BirthDate.Year - (BirthDate > DateTime.Today.AddYears(-(DateTime.Today.Year - BirthDate.Year)) ? 1 : 0);
}
