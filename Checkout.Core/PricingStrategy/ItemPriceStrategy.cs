using Checkout.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.PricingStrategy;

public interface IPricingStrategy
{
    int GetTotalPrice( IGrouping<string, Item> basket );

    //if the future requiement is products A+B bought together then this should be doing the grouping and pull items out of the basket maybe?
}

public class ItemPriceStrategy : IPricingStrategy
{
    public int GetTotalPrice( IGrouping<string, Item> basket ) => basket.Sum( x => x.Cost );
}

public class BuyXForYPricingStrategy(int NumberOfItems, int Amount) : IPricingStrategy
{
    public int GetTotalPrice( IGrouping<string, Item> basket )
    {
        throw new NotImplementedException();
    }
}
