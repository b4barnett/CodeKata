using Checkout.Core;
using Checkout.Core.PricingStrategy;
using Checkout.Entities;
using FluentAssertions;
using Moq;

namespace Kata.Tests;

public class Tests
{
    private ICheckout _service;
    private Dictionary<string, int> _itemPrices;
    private Dictionary<string, IPricingStrategy> _pricingStrategies;

    [SetUp]
    public void Setup()
    {
        _itemPrices = SetupItemPrices();
        _pricingStrategies = new Dictionary<string, IPricingStrategy>();
        _service = new Checkout.Core.Checkout(_itemPrices, _pricingStrategies, new ItemPriceStrategy());
    }

    //TODO: Potentiailly split some of these tests into different files

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
        var service = new Checkout.Core.Checkout( itemPrices.Object, new Dictionary<string, IPricingStrategy>(), new ItemPriceStrategy() );
        service.Scan( "A" );
        itemPrices.Verify(x => x.ContainsKey( "A" ), Times.Once);
    }

    [Test]
    public void Scan_Should_Return_False_WhenItemPriceMissing()
    {
        _service.Scan( "!!!!!!!!!!!!!!!" ).Should().BeFalse();
    }

    [Test]
    public void Should_Use_Default_PricingStrategy()
    {
        var defaultPricingStrategy = new Mock<IPricingStrategy>();
        var service = new Checkout.Core.Checkout( _itemPrices, new Dictionary<string, IPricingStrategy>(), defaultPricingStrategy.Object );
        service.Scan( "A" );
        service.GetTotalPrice();
        defaultPricingStrategy.Verify( x => x.GetTotalPrice( It.IsAny<IGrouping<string, Item>>() ), Times.Once );
    }

    //should this be combined with the above one?
    [TestCase(new[] { "C" }, 4, Description = "Single item of BOGOF")]
    [TestCase(new[] { "C", "C" }, 4, Description = "BOGOF")]
    [TestCase(new[] { "A", "C", "B", "C" }, 7, Description = "Out of order BOGOF with other items")]
    [TestCase(new[] { "C", "C", "C" }, 8, Description = "BOGOF with extra item not part of deal, terrible decision making really")]
    [TestCase(new[] { "D", "D", "D" }, 20, Description = "Buy 3 for 20")]
    [TestCase(new[] { "D", "D" }, 16, Description = "Buy 3 for 20, but too few items")]
    [TestCase(new[] { "D", "C", "D", "C", "D" }, 24, Description = "Buy 3 for 20 out of order, BOGOF out of order")]
    [TestCase(new[] { "A", "D", "A", "C", "B", "D", "B", "C", "D", "B" }, 24, Description = "Buy 3 for 20 out of order, BOGOF out of order, some items without pricing strats")]
    public void Products_With_PricingRules(string[] products, int expectedPrice)
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
        itemPrices.Add( "C", 4 );
        itemPrices.Add( "D", 8 );
        return itemPrices;
    }

    private static Dictionary<string, IPricingStrategy> SetupPricingStrategies()
    {
        var strats =  new Dictionary<string, IPricingStrategy>();
        return strats;
    }
}