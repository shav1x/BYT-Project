using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class ProductTests
{
    [SetUp]
    public void Setup()
    {
        Product.LoadExtent(new List<Product>());
    }

    [Test]
    public void Constructor_AddsProductToExtent()
    {
        var p = new Product("Test", 10, true, "Desc");

        Assert.That(Product.Extent.Count, Is.EqualTo(1));
        Assert.That(Product.Extent[0], Is.SameAs(p));
    }

    [Test]
    public void AddChild_UpdatesReverseParent()
    {
        var parent = new Product("Parent", 50, true);
        var child = new Product("Child", 20, true);

        parent.AddChild(child);

        Assert.Contains(child, parent.Children.ToList());
        Assert.Contains(parent, child.Parents.ToList());
    }

    [Test]
    public void AddParent_UpdatesReverseChild()
    {
        var parent = new Product("Parent", 50, true);
        var child = new Product("Child", 20, true);

        child.AddParent(parent);

        Assert.Contains(parent, child.Parents.ToList());
        Assert.Contains(child, parent.Children.ToList());
    }

    [Test]
    public void RemoveChild_UpdatesReverseParent()
    {
        var parent = new Product("Parent", 50, true);
        var child = new Product("Child", 20, true);

        parent.AddChild(child);
        parent.RemoveChild(child);

        Assert.IsFalse(parent.Children.Contains(child));
        Assert.IsFalse(child.Parents.Contains(parent));
    }

    [Test]
    public void RemoveParent_UpdatesReverseChild()
    {
        var parent = new Product("Parent", 50, true);
        var child = new Product("Child", 20, true);

        child.AddParent(parent);
        child.RemoveParent(parent);

        Assert.IsFalse(child.Parents.Contains(parent));
        Assert.IsFalse(parent.Children.Contains(child));
    }

    [Test]
    public void AddChild_SelfReference_ThrowsException()
    {
        var p = new Product("Test", 10, true);

        Assert.Throws<InvalidOperationException>(() => p.AddChild(p));
    }

    [Test]
    public void Name_InvalidCharacters_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var p = new Product("Invalid!Name", 10, true);
        });
    }

    [Test]
    public void RemoveChild_NonExisting_DoesNotThrow()
    {
        var parent = new Product("Parent", 50, true);
        var child = new Product("Child", 20, true);

        Assert.DoesNotThrow(() => parent.RemoveChild(child));
    }
}
