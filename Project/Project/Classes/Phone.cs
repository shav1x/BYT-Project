using System.Text.RegularExpressions;

namespace Project.Classes;

public class Phone
{
    public string CountryCode { get; private set; }
    public string Number { get; private set; }
    
    public Phone() { }

    public Phone(string countryCode, string number)
    {
        if (!Regex.IsMatch(countryCode, @"^\+?[0-9]+$"))
            throw new ArgumentException("Invalid country code");

        if (!Regex.IsMatch(number, @"^[0-9]+$"))
            throw new ArgumentException("Invalid phone number");

        CountryCode = countryCode.Trim();
        Number = number.Trim();
    }

    public override string ToString() => $"{CountryCode}{Number}";
}