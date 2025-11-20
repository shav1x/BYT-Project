using System.Text.RegularExpressions;

namespace Project.Classes;

public class Movie
{
    private static readonly List<Movie> _extent = new();
    public static IReadOnlyList<Movie> Extent => _extent.AsReadOnly();

    private string _name = null!;
    private decimal _duration;
    private List<Genre> _genres = new();
    private List<string>? _languages;
    private DateTime _releaseDate;
    
    private static readonly int DefaultMinAge = 3;
    private int _minAge;

    public int MinAge
    {
        get => _minAge;
        set
        {
            if (value < DefaultMinAge)
                throw new ArgumentException("Minimum age cannot be less than 3.");

            _minAge = value;
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[A-Za-z ]+$"))
            {
                throw new ArgumentException("Name must contain only letters.");
            }

            _name = value;
        }
    }

    public decimal Duration
    {
        get => _duration;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Duration must be greater than 0.");

            _duration = value;
        }
    }

    public List<Genre> Genres
    {
        get => _genres;
        set
        {
            if (value is null || value.Count < 1)
                throw new ArgumentException("Movie must have at least one genre.");

            _genres = value;
        }
    }

    public DateTime ReleaseDate
    {
        get => _releaseDate;
        set
        {
            if (value > DateTime.Today)
                throw new ArgumentException("Release date cannot be in the future.");

            _releaseDate = value;
        }
    }

    public List<string>? Languages
    {
        get => _languages;
        set
        {
            if (value is null)
            {
                _languages = null;
                return;
            }

            foreach (var lang in value)
            {
                if (!Regex.IsMatch(lang, @"^[A-Za-z]+$"))
                    throw new ArgumentException("Language must contain only letters.");
            }

            _languages = value;
        }
    }
    
    public Movie()
    {
        _genres = new List<Genre>();
        _languages = new List<string>();
    }
    
    public Movie(
        string name,
        decimal duration,
        List<Genre> genres,
        DateTime releaseDate,
        List<string>? languages,
        int? minAge
    )
    {
        Name = name;
        Duration = duration;
        Genres = genres;
        ReleaseDate = releaseDate;
        Languages = languages;
        MinAge = minAge ?? DefaultMinAge;

        _extent.Add(this);
    }
    
    public static void LoadExtent(List<Movie>? movies)
    {
        _extent.Clear();

        if (movies is null || movies.Count == 0)
            return;

        _extent.AddRange(movies);
    }
}