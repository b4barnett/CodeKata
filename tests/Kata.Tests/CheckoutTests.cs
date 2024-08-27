using Checkout.Core;
using Checkout.Entities;
using FluentAssertions;

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

    private static Dictionary<string, int> SetupItemPrices()
    {
        var itemPrices = new Dictionary<string, int>();
        itemPrices.Add( "A", 1 );
        itemPrices.Add( "B", 2 );
        return itemPrices;
    }
}