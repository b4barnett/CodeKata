using Checkout.Core.PricingStrategy;
using Checkout.Entities;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata.Tests;

internal class BuyXForYPricingStrategyTests
{
    private const int CostPerItem = 50;
    private const int BundleCost = 80;


    [TestCase(1, 2, 50, Description = "not enough for the bundle")]
    [TestCase(2, 2, 80, Description = "Exact bundle items")]
    [TestCase(3, 2, 130, Description = "1 more than the bundle items")]
    [TestCase(4, 2, 160, Description = "double bundle")]
    [TestCase(1, 3, 50, Description = "three item bundle, 1 item")]
    [TestCase(2, 3, 100, Description = "three item bundle, 2 items")]
    [TestCase(3, 3, 80, Description = "three item bundle, 3 items")]
    [TestCase(4, 3, 130, Description = "three item bundle, 4 items")]
    public void XFoY_StrategyTests( int numberOfItems, int numberItemsInBundle, int expectedCost )
    {
        BuyXForYPricingStrategy strategy = new BuyXForYPricingStrategy( numberItemsInBundle, BundleCost );
        var items = Enumerable.Range(0, numberOfItems).Select(x => new Item("A", CostPerItem)).GroupBy(x => x.Sku).Single();

        strategy.GetTotalPrice(items).Should().Be( expectedCost );
    }

    [Test(Description = "Empty basket")]
    public void XFoY_StrategyTests_EmptyBaskey()
    {
        BuyXForYPricingStrategy strategy = new BuyXForYPricingStrategy( 1, BundleCost );
        strategy.GetTotalPrice( new EmptyGrouping() );
    }
}

internal class EmptyGrouping : IGrouping<string, Item>
{
    public string Key => "";

    public IEnumerator<Item> GetEnumerator()
    {
        return new List<Item>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new List<Item>().GetEnumerator();
    }
}
