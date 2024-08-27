using Checkout.Entities;

namespace Checkout.Core.PricingStrategy;

public class ItemPriceStrategy : IPricingStrategy
{
    public int GetTotalPrice( IGrouping<string, Item> basket ) => basket.Sum( x => x.Cost );
}
