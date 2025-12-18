namespace Project.Classes;

public class Staff
{
    private static readonly List<Staff> _extent = new();
    public static IReadOnlyList<Staff> Extent => _extent.AsReadOnly();

    private decimal _salary;
    private string _role = null!;
    public Person Person { get; set; } = null!;

    public decimal Salary
    {
        get => _salary;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Salary must be positive");

            _salary = value;
        }
    }

    public string Role
    {
        get => _role;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Role cannot be empty");

            _role = value;
        }
    }
    
    public Staff()
    {
    }

    public Staff(
        Person person,
        string role,
        decimal salary
    )
    {
        Person = person ?? throw new ArgumentNullException(nameof(person));
        Role = role;
        Salary = salary;

        _extent.Add(this);
    }

    public static void LoadExtent(List<Staff>? staffMembers)
    {
        _extent.Clear();

        if (staffMembers is null || staffMembers.Count == 0)
            return;

        _extent.AddRange(staffMembers);
    }
}