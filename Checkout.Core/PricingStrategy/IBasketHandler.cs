using Checkout.Entities;

namespace Checkout.Core.PricingStrategy;

public interface IBasketHandler
{
    Basket UpdateTotal( Basket basket );
}

//Just an idea I had after finishing the project, instead of calculating for each group the basket would be passed to each handler which returns a new basket,
//it would remove items relevant to just it's "specification" and would leave items that wouldn't fit, for items A, B, B, B, C, for a BOGOFF would result in A, B, C being returned,
//the two BOGOFF being removed and leaving the one that doesn't fit the deal, the final/default would be an item price that would just remove and sum the cost of all the items

//If I get chance this evening to implement this I might but I've got some things to deal with this afternoon
