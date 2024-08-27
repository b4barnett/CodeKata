using Checkout.Core;
using Checkout.Entities;
using FluentAssertions;
using Moq;

namespace Kata.Tests;

public class Tests
{
    private ICheckout _service;
    private Dictionary<string, int> _itemPrices;

    [SetUp]
    public void Setup()
    {
        _itemPrices = SetupItemPrices();
        _service = new Checkout.Core.Checkout(_itemPrices);
    }

    [TestCase(new string[0], 0, Description = "Empty cart should be zero")]
    [TestCase(new[] { "A" }, 1, Description = "Single item with no pricing rules should return item price")]
    [TestCase(new[] { "A", "A" }, 2, Description = "Two of the same item with no pricing rules should return double the item price")]
    [TestCase(new[] { "A", "B" }, 3, Description = "Two items with no pricing rules should return summed value")]
    public void Products_With_NoPricingRules_ReturnsSummed(string[] products, int expectedPrice)
    {
        foreach ( var product in products )
        {
            _service.Scan( product );
        }
        _service.GetTotalPrice().Should().Be( expectedPrice );
    }

    [Test]
    public void Scan_Should_Check_ItemPrice()
    {
        Mock<IDictionary<string, int>> itemPrices = new Mock<IDictionary<string, int>>();
        var service = new Checkout.Core.Checkout( itemPrices.Object );
        service.Scan( "A" );
        itemPrices.Verify(x => x.ContainsKey( "A" ), Times.Once);
    }

    [Test]
    public void Scan_Should_Return_False_WhenItemPriceMissing()
    {
        _service.Scan( "!!!!!!!!!!!!!!!" ).Should().BeFalse();
    }

    private static Dictionary<string, int> SetupItemPrices()
    {
        var itemPrices = new Dictionary<string, int>();
        itemPrices.Add( "A", 1 );
        itemPrices.Add( "B", 2 );
        return itemPrices;
    }
}