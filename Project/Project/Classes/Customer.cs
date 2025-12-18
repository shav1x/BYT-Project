namespace Project.Classes;

public class Customer
{
    private static readonly List<Customer> _extent = new();
    public static IReadOnlyList<Customer> Extent => _extent.AsReadOnly();
    
    private static int _lastAccountId = 0;
    public int AccountId { get; set; }
    public Person Person { get; set; } = null!;

    private int _bonusPoints;
    public int BonusPoints
    {
        get => _bonusPoints;
        set
        {
            if (value < 0)
                throw new ArgumentException("Bonus points cannot be negative");

            _bonusPoints = value;
        }
    }
    
    public Customer()
    {
    }

    public Customer(
        Person person
    )
    {
        Person = person ?? throw new ArgumentNullException(nameof(person));
        AccountId = ++_lastAccountId;
        BonusPoints = 0;

        _extent.Add(this);
    }

    public static void LoadExtent(List<Customer>? customers)
    {
        _extent.Clear();
        if (customers is null || customers.Count == 0)
        {
            _lastAccountId = 0;
            return;
        }

        _extent.AddRange(customers);
        
        _lastAccountId = _extent.Max(c => c.AccountId);
    }
}