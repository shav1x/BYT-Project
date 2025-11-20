namespace Project.Classes;

public class Customer : Person
{
    private static List<Customer> _customers = new List<Customer>();
    
    private static int _customerCount = 0;
    public readonly int AccountId = _customerCount + 1;
    
    private int _bonusPoints;
    public int BonusPoints
    {
        get => _bonusPoints;
        set{
            if (value < 0)
            {
                throw new ArgumentException("Bonus points cannot be negative");
            }
            
            _bonusPoints += value;
        }
    }

    public Customer(string name, string surname, DateTime dateOfBirth, string email, string? phone) : base(name, surname, dateOfBirth, email, phone)
    {
        BonusPoints = 0;
        _customerCount++;
        _customers.Add(this);
    }
}
