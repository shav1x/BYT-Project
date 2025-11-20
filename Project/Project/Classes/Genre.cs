using System.Text.RegularExpressions;

namespace Project.Classes;

public class Genre
{
    private static List<Genre> _genres = new List<Genre>();
    
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[A-Za-z]+$"))
            {
                throw new ArgumentException("Genre name cannot be empty.");
            }
        }
    }

    public Genre(string name)
    {
        Name = name;
        _genres.Add(this);
    }
}