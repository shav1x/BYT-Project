using System.Text.RegularExpressions;

namespace Project.Classes;

public class Genre
{
    private static readonly List<Genre> _extent = new();
    public static IReadOnlyList<Genre> Extent => _extent.AsReadOnly();

    private string _name = null!;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[A-Za-z]+$"))
            {
                throw new ArgumentException("Genre name must contain only letters.");
            }

            _name = value;
        }
    }

    public Genre() { }

    public Genre(string name)
    {
        Name = name;
        _extent.Add(this);
    }
    
    public static void LoadExtent(List<Genre>? genres)
    {
        _extent.Clear();
        if (genres != null)
            _extent.AddRange(genres);
    }
}