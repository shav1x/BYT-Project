using System.Xml.Serialization;

namespace Project.Classes;

[Serializable]
[XmlRoot("Customer")]
public class Customer : Person
{
    private static int _customerCount = 0;
    [XmlAttribute("id")]
    public readonly int AccountId = _customerCount + 1;

    [XmlElement("BonusPoints")]
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

    public Customer() : base()
    {
    }

    public Customer(string name, string surname, DateTime dateOfBirth, string email, string? phone) : base(name, surname, dateOfBirth, email, phone)
    {
        _customerCount++;
    }
}
