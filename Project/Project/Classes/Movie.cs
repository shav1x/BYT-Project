using System.Text.RegularExpressions;

namespace Project.Classes;

public class Movie
{
    private string _name;
    private decimal _duration;
    private List<Genre> _genres = new ();
    private List<String>? _language = new ();
    private DateTime _releaseDate;
    private int _minAge;

    public int MinAge
    {
        get => _minAge;
        set
        {
            if (value < 3)
            {
                throw new ArgumentException("Minimum age cannot be less than 3.");
            }
            _minAge = value;
        }
    }
    
    public required string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name must contain only letters.");
            }

            _name = value;
        }
    }

    public required decimal Duration
    {
        get => _duration;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Duration must be greater than 0.");
            }
            _duration = value;
        }
    }

    public required List<Genre> Genres
    {
        get => _genres;
        set
        {
            if (value.Count < 1)
            {
                throw new ArgumentException("Movie must have at least one genre.");
            }
            
            _genres = value;
        }
    }

    public required DateTime ReleaseDate
    {
        get => _releaseDate;
        set
        {
            if (value > DateTime.Today)
            {
                throw new ArgumentException("Release date cannot be in the future.");
            }
            _releaseDate = value;
        }
    }

    public List<String>? Languages
    {
        get => _language;
        set
        {
            if (value == null) return;
            for (int i = 0; i < value.Count; i++)
            {
                if (!Regex.IsMatch(value[i], @"^[A-Za-z]+$"))
                {
                    throw new ArgumentException("Language must contain only letters.");
                }
            }
            _language = value;
        }
    }
    
    public Movie(string name, decimal duration, List<Genre> genres, DateTime releaseDate, List<string>? languages, int? minAge)
    {
        Name = name;
        Duration = duration;
        Genres = genres;
        ReleaseDate = releaseDate;
        Languages = languages;
        MinAge = minAge ?? 3;
    }
}