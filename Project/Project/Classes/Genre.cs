using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Project.Classes;

public class Genre
{
    private static readonly List<Genre> _extent = new();
    public static IReadOnlyList<Genre> Extent => _extent.AsReadOnly();

    private string _name = null!;
    private readonly List<Movie> _movies = new();
    
    [JsonIgnore] 
    public List<Movie> Movies => _movies;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[A-Za-z ]+$"))
            {
                throw new ArgumentException("Genre name must contain only letters and spaces.");
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

    public void AddMovie(Movie movie)
    {
        if (movie == null) throw new ArgumentNullException(nameof(movie));
        if (_movies.Contains(movie)) return;

        _movies.Add(movie);
    }

    public void RemoveMovie(Movie movie)
    {
        _movies.Remove(movie);
    }

    public static void LoadExtent(List<Genre>? genres)
    {
        _extent.Clear();
        if (genres != null)
            _extent.AddRange(genres);
    }
}