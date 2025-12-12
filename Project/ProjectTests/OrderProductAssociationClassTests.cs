namespace ProjectTests;

using System;
using NUnit.Framework;
using Project.Classes;

[TestFixture]
public class OrderProductAssociationTests
{
    private Product _product;
    private Order _order;

    [SetUp]
    public void Setup()
    {
        Product.LoadExtent(null);
        Order.LoadExtent(null);
        OrderItem.LoadExtent(null);

        _product = new Product("Laptop", 1000m, true, "High-end laptop");
        _order = new Order(DateTime.Now);
    }

    [Test]
    public void AddOrder_ShouldCreateOrderItemAndLinkBothWays()
    {
        var item = _product.AddOrder(_order, 2);

        Assert.That(_product.OrderItems.Count, Is.EqualTo(1));
        Assert.That(_product.OrderItems[0], Is.EqualTo(item));

        Assert.That(_order.OrderItems.Count, Is.EqualTo(1));
        Assert.That(_order.OrderItems[0], Is.EqualTo(item));

        Assert.That(item.Product, Is.EqualTo(_product));
        Assert.That(item.Order, Is.EqualTo(_order));
        Assert.That(item.Quantity, Is.EqualTo(2));
    }

    [Test]
    public void AddOrder_SameProductTwice_ShouldReturnExistingOrderItem()
    {
        var firstItem = _product.AddOrder(_order, 2);
        var secondItem = _product.AddOrder(_order, 2);

        Assert.That(secondItem, Is.EqualTo(firstItem));
        Assert.That(_product.OrderItems.Count, Is.EqualTo(1));
        Assert.That(_order.OrderItems.Count, Is.EqualTo(1));
    }

    [Test]
    public void RemoveOrder_ShouldUnlinkProductAndOrder()
    {
        var item = _product.AddOrder(_order, 2);
        _product.RemoveOrder(_order);

        Assert.IsEmpty(_product.OrderItems);
        Assert.IsEmpty(_order.OrderItems);
    }

    [Test]
    public void TotalPrice_ShouldCalculateCorrectly()
    {
        _product.AddOrder(_order, 2);
        var product2 = new Product("Mouse", 50m, true);
        product2.AddOrder(_order, 3);

        Assert.That(_order.TotalPrice, Is.EqualTo(1000m * 2 + 50m * 3));
    }

    [Test]
    public void AddingNullOrder_ShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() => _product.AddOrder(null!, 1));
    }

    [Test]
    public void AddingOrderItemWithZeroQuantity_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => _product.AddOrder(_order, 0));
    }
}
