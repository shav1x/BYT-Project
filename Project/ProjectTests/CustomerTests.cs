using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class CustomerTests
{
    [Test]
    public void Constructor_ValidData_CreatesCustomer()
    {
        var phone = new Phone("+48", "712345678");

        var customer = new Person("Ben", "Ten", DateTime.Now.AddYears(-20),
            "mail@gmail.com", phone);

        Assert.That(customer.Name, Is.EqualTo("Ben"));
        Assert.That(customer.Surname, Is.EqualTo("Ten"));
        Assert.That(customer.Email, Is.EqualTo("mail@gmail.com"));
        Assert.That(customer.Phone!.CountryCode, Is.EqualTo("+48"));
        Assert.That(customer.Phone!.Number, Is.EqualTo("712345678"));
        Assert.That(customer.Age, Is.EqualTo(19));
    }

    [TestCase("John")]
    [TestCase("Ben")]
    [TestCase("Jester")]
    [TestCase("name")]
    public void CustomerName_ValidName_True(string name)
    {
        var customer = new Person(name, "Smith", DateTime.Now.AddYears(-20),
            "mail@gmail.com", new Phone("+1", "2345678"));

        Assert.That(customer.Name, Is.EqualTo(name));
    }

    [TestCase("!1284*!ˆ%#")]
    [TestCase("John123")]
    [TestCase("some name")]
    [TestCase("")]
    public void CustomerName_InvalidName_ThrowsArgumentException(string name)
    {
        Assert.That(
            () => new Person(name, "Smith", DateTime.Now.AddYears(-20),
                "mail@gmail.com", new Phone("+1", "2345678")),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [TestCase("someemail@hmail.com")]
    [TestCase("apple@apple.com")]
    [TestCase("rand0mmail123@me.com.ua")]
    [TestCase("sdsashiosdfn@mail.ua")]
    public void CustomerEmail_ValidEmail_True(string email)
    {
        var customer = new Person("John", "Smith", DateTime.Now.AddYears(-20),
            email, new Phone("+1", "2345678"));

        Assert.That(customer.Email, Is.EqualTo(email));
    }

    [TestCase("email")]
    [TestCase("mail.com")]
    [TestCase("@com")]
    [TestCase("mail@gmail.")]
    [TestCase("")]
    public void CustomerEmail_InvalidEmail_ThrowsArgumentException(string email)
    {
        Assert.That(
            () => new Person("John", "Smith", DateTime.Now.AddYears(-20),
                email, new Phone("+1", "2345678")),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [Test]
    public void CustomerPhone_ValidPhone_AssignedCorrectly()
    {
        var phone = new Phone("+1", "55555");
        var customer = new Person("Ben", "Ten", DateTime.Now.AddYears(-20), "mail@gmail.com", phone);

        Assert.That(customer.Phone, Is.EqualTo(phone));
    }
    
    [Test]
    public void CustomerPhone_NullPhone_AssignedCorrectly()
    {
        var customer = new Person("Ben", "Ten", DateTime.Now.AddYears(-20), "mail@gmail.com", null);
        Assert.That(customer.Phone, Is.Null);
    }
}
