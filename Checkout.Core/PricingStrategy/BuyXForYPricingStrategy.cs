using Checkout.Entities;

namespace Checkout.Core.PricingStrategy;

public class BuyXForYPricingStrategy(int NumberOfItemsInBundle, int Amount) : IPricingStrategy
{
    public int GetTotalPrice( IGrouping<string, Item> basket )
    {
        if ( basket.Count() == 0 )
        {
            return 0;
        }
        int bundles = basket.Count() / NumberOfItemsInBundle;
        int bundleCost = bundles * Amount;
        int remainder = basket.Count() % NumberOfItemsInBundle;
        int remainderCost = remainder * basket.First().Cost;

        return bundleCost + remainderCost;
    }
}
