using NUnit.Framework;
using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class PhoneTests
{
    // -------------------------------
    // VALID COUNTRY CODE TESTS
    // -------------------------------

    [TestCase("+380")]
    [TestCase("+1")]
    [TestCase("380")]
    [TestCase("1")]
    [TestCase("0")]
    [TestCase("48")]
    [TestCase("+48")]
    public void CountryCode_Valid_ShouldCreatePhone(string countryCode)
    {
        var phone = new Phone(countryCode, "1234567");

        Assert.That(phone.CountryCode, Is.EqualTo(countryCode.Trim()));
    }

    // -------------------------------
    // INVALID COUNTRY CODE TESTS
    // -------------------------------

    [TestCase("++380")]
    [TestCase("38-0")]
    [TestCase("A380")]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(" +380")]
    public void CountryCode_Invalid_ShouldThrow(string countryCode)
    {
        Assert.That(
            () => new Phone(countryCode, "1234567"),
            Throws.TypeOf<ArgumentException>()
        );
    }

    // -------------------------------
    // VALID PHONE NUMBER TESTS
    // -------------------------------

    [TestCase("1234567")]
    [TestCase("55555")]
    [TestCase("9328421")]
    [TestCase("0")]
    [TestCase("111111111")]
    public void Number_Valid_ShouldCreatePhone(string number)
    {
        var phone = new Phone("+380", number);

        Assert.That(phone.Number, Is.EqualTo(number.Trim()));
    }

    // -------------------------------
    // INVALID PHONE NUMBER TESTS
    // -------------------------------

    [TestCase("123A56")]
    [TestCase("123-4567")]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("12 34")]
    public void Number_Invalid_ShouldThrow(string number)
    {
        Assert.That(
            () => new Phone("+380", number),
            Throws.TypeOf<ArgumentException>()
        );
    }

    // -------------------------------
    // ToString() TEST
    // -------------------------------

    [Test]
    public void ToString_ReturnsCountryCodePlusNumber()
    {
        var phone = new Phone("+380", "987654321");

        Assert.That(phone.ToString(), Is.EqualTo("+380987654321"));
    }
}
