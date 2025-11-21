namespace ProjectTests;

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project.Classes;



[TestFixture]
public class MovieTests
{
    private Genre _actionGenre = new Genre("Action");
    
    [Test]
    public void Constructor_ValidData_CreatesMovie()
    {
        var movie = new Movie(
            name: "Vitalik",
            duration: 120,
            genres: new List<Genre> { _actionGenre },
            releaseDate: new DateTime(2020, 1, 1),
            languages: new List<string> { "English", "French" },
            minAge: 12
        );

        Assert.That(movie.Name, Is.EqualTo("Vitalik"));
        Assert.That(movie.Duration, Is.EqualTo(120));
        Assert.That(movie.Genres.Count, Is.EqualTo(1));
        Assert.That(movie.MinAge, Is.EqualTo(12));
        Assert.That(movie.Languages?.Count, Is.EqualTo(2));
    }
    
    [TestCase("")]
    [TestCase(" ")]
    public void Name_Invalid_ThrowsArgumentException(string invalidName)
    {
        Assert.Throws<ArgumentException>(() => new Movie(
            invalidName,
            100,
            new List<Genre> { _actionGenre },
            new DateTime(2020,1,1),
            new List<string>{ "English" },
            3
        ));
    }
    
    [TestCase(0)]
    [TestCase(-50)]
    public void Duration_Invalid_ThrowsArgumentException(decimal invalidDuration)
    {
        Assert.Throws<ArgumentException>(() => new Movie(
            "Vitalik",
            invalidDuration,
            new List<Genre> { _actionGenre },
            new DateTime(2020,1,1),
            new List<string>{ "English" },
            3
        ));
    }
    
    [Test]
    public void Genres_EmptyList_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Movie(
            "Vitalik",
            120,
            new List<Genre>(),
            new DateTime(2020,1,1),
            new List<string>{ "English" },
            3
        ));
    }
    
    [Test]
    public void ReleaseDate_Future_ThrowsArgumentException()
    {
        var futureDate = DateTime.Today.AddDays(1);
        Assert.Throws<ArgumentException>(() => new Movie(
            "Vitalik",
            120,
            new List<Genre> { _actionGenre },
            futureDate,
            new List<string>{ "English" },
            3
        ));
    }
    
    [TestCase("English1")]
    [TestCase("Fr@ench")]
    [TestCase("123")]
    public void Languages_Invalid_ThrowsArgumentException(string invalidLang)
    {
        Assert.Throws<ArgumentException>(() => new Movie(
            "Vitalik",
            120,
            new List<Genre> { _actionGenre },
            new DateTime(2020,1,1),
            new List<string>{ invalidLang },
            3
        ));
    }
    
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void MinAge_LessThan3_ThrowsArgumentException(int invalidAge)
    {
        Assert.Throws<ArgumentException>(() => new Movie(
            "Vitalik",
            120,
            new List<Genre> { _actionGenre },
            new DateTime(2020,1,1),
            new List<string>{ "English" },
            invalidAge
        ));
    }
    
    [Test]
    public void Constructor_NullMinAge_DefaultsTo3()
    {
        var movie = new Movie(
            "Vitalik",
            120,
            new List<Genre> { _actionGenre },
            new DateTime(2020,1,1),
            new List<string>{ "English" },
            null
        );

        Assert.That(movie.MinAge, Is.EqualTo(3));
    }
    
    [Test]
    public void Constructor_NullLanguages_AllowsNull()
    {
        var movie = new Movie(
            "Vitalik",
            120,
            new List<Genre> { _actionGenre },
            new DateTime(2020,1,1),
            null,
            3
        );

        Assert.IsNull(movie.Languages);
    }
}

