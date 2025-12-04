using System.Text.RegularExpressions;

namespace Project.Classes;

public class Auditorium
{
    private static readonly List<Auditorium> _extent = new();
    public static IReadOnlyList<Auditorium> Extent => _extent.AsReadOnly();
    
    private int _number;
    private string _name = null!;
    
    public int Number 
    {
        get => _number;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Auditorium number must be greater than 0.");
            _number = value;
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
    
    public Auditorium() { }
    
    public Auditorium(int number, string name)
    {
        Number = number;
        Name = name;
        _extent.Add(this);
    }
    
    public static void LoadExtent(List<Auditorium>? auditoriums)
    {
        _extent.Clear();

        if (auditoriums is null || auditoriums.Count == 0)
            return;

        _extent.AddRange(auditoriums);
    }
    
}
