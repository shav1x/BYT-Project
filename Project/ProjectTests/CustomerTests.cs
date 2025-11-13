using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class Tests
{
    
    [TestCase("John")]
    [TestCase("Ben")]
    [TestCase("abdula")]
    [TestCase("name")]
    public void CustomerName_ValidName_True(string name)
    {
        var customer = new Customer(name, "Smith", DateTime.Now.AddYears(-20), "mail@gmail.com", "0712345678");
        Assert.That(customer.Name, Is.EqualTo(name));
    }

    [TestCase("!1284*!ˆ%#")]
    [TestCase("John123")]
    [TestCase("some name")]
    [TestCase("")]
    public void CustomerName_InvalidName_ThrowsException(string name)
    {
        Assert.That(
            () => new Customer(name, "Smith", DateTime.Now.AddYears(-20), "mail@gmail.com", "0712345678"),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [TestCase("someemail@hmail.com")]
    [TestCase("apple@apple.com")]
    [TestCase("rand0mmail123@me.com.ua")]
    [TestCase("sdsashiosdfn@mail.ua")]
    public void CustomerEmail_ValidEmail_True(string email)
    {
        var customer = new Customer("John", "Smith", DateTime.Now.AddYears(-20), email, "0712345678");
        Assert.That(customer.Email, Is.EqualTo(email));
    }

    [TestCase("email")]
    [TestCase("mail.com")]
    [TestCase("@com")]
    [TestCase("mail@gmail.")]
    [TestCase("")]
    public void CustomerEmail_InvalidEmail_ThrowsException(string email)
    {
        Assert.That(
            () => new Customer("John", "Smith", DateTime.Now.AddYears(-20), email, "0712345678"),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [TestCase("02385717")]
    [TestCase("3248519")]
    [TestCase("238472")]
    [TestCase("56482")]
    public void CustomerPhone_ValidPhone_True(string phone)
    {
        var customer = new Customer("John", "Smith", DateTime.Now.AddYears(-20), "example@mail.com", phone);
        Assert.That(customer.Phone, Is.EqualTo(phone));
    }

    [TestCase("0A2385717")]
    [TestCase("32485F19")]
    [TestCase("+238472")]
    [TestCase("56-482-24")]
    public void CustomerPhone_InvalidPhone_ThrowsException(string phone)
    {
        Assert.That(
            () => new Customer("John", "Smith", DateTime.Now.AddYears(-20), "example@mail.com", phone),
            Throws.TypeOf<ArgumentException>()
        );
    }
    
}
