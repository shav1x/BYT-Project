namespace Project.Classes;

public class Customer : Person
{
    private static int _customerCount = 0;
    public readonly int AccountId = _customerCount + 1;

    public int BonusPoints { get; set; } = 0;

    public Customer()
    {
        _customerCount++;
    }
}