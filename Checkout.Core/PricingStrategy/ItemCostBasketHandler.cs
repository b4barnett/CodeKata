using Checkout.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.PricingStrategy;

public class ItemCostBasketHandler : IBasketHandler
{
    public Basket UpdateTotal( Basket basket ) => new Basket(ImmutableList<Item>.Empty, basket.CurrentTotal + basket.Items.Sum( item => item.Cost ) );
}
