﻿using Checkout.Core.PricingStrategy;
using Checkout.Entities;
using FluentAssertions;

namespace Kata.Tests;

internal class ItemPriceStrategyTests
{
    private ItemPriceStrategy _strategy;

    [SetUp]
    public void Setup()
    {
        _strategy = new ItemPriceStrategy();
    }

    [Test]
    public void ItemPriceStrategy_Returns_CorrectSummation()
    {
        List<Item> items = new List<Item>()
        {
            new Item("A", 200),
            new Item("A", 300),
            new Item("A", 500)
        };
        _strategy.GetTotalPrice( items.GroupBy( x => x.Sku ).Single() ).Should().Be( 1000 );
    }
}
