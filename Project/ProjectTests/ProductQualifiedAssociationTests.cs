using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class ProductQualifiedAssociationTests
{
    
    [Test]
    public void AddProduct_ShouldAssociateProductWithCatalog_BothDirections()
    {
        var catalog = new ProductCatalog("Catalog1");
        var product = new Product("ProductA", 99.99m, true);
        
        catalog.AddProduct(product);
        
        Assert.That(catalog.Products.ContainsKey(product.Name));
        Assert.That(catalog.Products[product.Name], Is.EqualTo(product));
        Assert.That(product.Catalog, Is.EqualTo(catalog));
    }

    [Test]
    public void AddToProductCatalog_ShouldAssociateProductWithCatalog_BothDirections()
    {
        var catalog = new ProductCatalog("Catalog1");
        var product = new Product("ProductA", 19.99m, true);
        
        product.AddToProductCatalog(catalog);
        
        Assert.That(product.Catalog, Is.EqualTo(catalog));
        Assert.That(catalog.Products.ContainsValue(product));
        Assert.That(catalog.Products.ContainsKey(product.Name));
    }

    [Test]
    public void RemoveProduct_ShouldRemoveAssociations_BothDirections()
    {
        var catalog = new ProductCatalog("Catalog1");
        var product = new Product("ProductB", 39.99m, true);
        catalog.AddProduct(product);
        
        catalog.RemoveProduct(product);
        
        Assert.That(catalog.Products.ContainsKey(product.Name), Is.False);
        Assert.That(product.Catalog, Is.Null);
    }

    [Test]
    public void RemoveFromProductCatalog_ShouldRemoveAssociations_BothDirections()
    {
        var catalog = new ProductCatalog("Catalog1");
        var product = new Product("ProductC", 9.99m, true);
        product.AddToProductCatalog(catalog);
        
        product.RemoveFromProductCatalog(catalog);
        
        Assert.That(catalog.Products.ContainsKey(product.Name), Is.False);
        Assert.That(product.Catalog, Is.Null);
    }

    [Test]
    public void AddingProductWithDuplicateName_ThrowsException()
    {
        var catalog = new ProductCatalog("CatalogX");
        var product1 = new Product("ProductY", 49.99m, true);
        var product2 = new Product("ProductY", 59.99m, false);

        catalog.AddProduct(product1);

        Assert.Throws<ArgumentException>(() => catalog.AddProduct(product2));
    }

    [Test]
    public void AddingProductToAnotherCatalog_ThrowsException()
    {
        var catalog1 = new ProductCatalog("CatalogA");
        var catalog2 = new ProductCatalog("CatalogB");
        var product = new Product("ProductZ", 79.99m, true);

        catalog1.AddProduct(product);

        Assert.Throws<InvalidOperationException>(() => product.AddToProductCatalog(catalog2));
    }

    [Test]
    public void RemovingNonexistentProduct_ThrowsException()
    {
        var catalog = new ProductCatalog("CatalogTest");
        var product = new Product("NotAddedProduct", 10.1m, true);

        Assert.Throws<ArgumentException>(() => catalog.RemoveProduct(product));
    }

}
