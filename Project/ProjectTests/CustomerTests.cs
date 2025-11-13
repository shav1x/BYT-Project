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
    
}