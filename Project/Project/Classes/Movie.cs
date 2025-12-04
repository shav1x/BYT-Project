using System.Text.RegularExpressions;

namespace Project.Classes;

public class Movie
{
    private static readonly List<Movie> _extent = new();
    public static IReadOnlyList<Movie> Extent => _extent.AsReadOnly();
    
    private string _name = null!;
    private decimal _duration;
    
    public List<Genre> Genres { get; set; } = new();

    private List<string>? _languages = new();
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
    private DateTime _releaseDate;

    private static readonly int DefaultMinAge = 3;
    private int _minAge = DefaultMinAge;

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
                throw new ArgumentException("Name must contain only letters and spaces.");
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


    
    public Movie()
    {
    }

    public Movie(string name, decimal duration, IEnumerable<Genre> genres, DateTime releaseDate, IEnumerable<string>? languages, int? minAge = null)
    {
        Name = name;
        Duration = duration;
        ReleaseDate = releaseDate;
        MinAge = minAge ?? 3;

        if (genres == null || !genres.Any())
            throw new ArgumentException("Movie must have at least one genre.");

        Genres.AddRange(genres);

        foreach (var genre in genres)
        {
            genre.AddMovie(this);
        }

        if(languages == null)
            _languages = null;
        else
        {
            foreach (var lang in languages)
            {
                AddLanguage(lang);
            }
        }
        
        _extent.Add(this);
    }

    public void AddGenre(Genre genre)
    {
        if (genre == null) throw new ArgumentNullException(nameof(genre));
        if (Genres.Contains(genre)) return;

        Genres.Add(genre);
        genre.AddMovie(this);
    }

    public void RemoveGenre(Genre genre)
    {
        if (genre == null) throw new ArgumentNullException(nameof(genre));
        if (!Genres.Remove(genre)) return;

        genre.RemoveMovie(this);
    }
    
    public void AddLanguage(string language)
    {
        if (string.IsNullOrWhiteSpace(language) ||
            !Regex.IsMatch(language, @"^[A-Za-z]+$"))
        {
            throw new ArgumentException("Language must contain only letters.", nameof(language));
        }

        if (!_languages.Contains(language))
            _languages.Add(language);
    }

    public void RemoveLanguage(string language)
    {
        _languages.Remove(language);
    }
    
    public static void LoadExtent(List<Movie>? movies)
    {
        _extent.Clear();

        if (movies is null || movies.Count == 0)
            return;

        _extent.AddRange(movies);
    }
}