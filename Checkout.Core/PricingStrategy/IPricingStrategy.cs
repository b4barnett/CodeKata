using Checkout.Entities;

namespace Checkout.Core.PricingStrategy;

public interface IPricingStrategy
{
    int GetTotalPrice( IGrouping<string, Item> basket );

    //if the future requiement is products A+B bought together then this should be doing the grouping and pull items out of the basket maybe?
}

//