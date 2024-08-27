using Checkout.Core.PricingStrategy;
using Checkout.Entities;

namespace Checkout.Core;

public interface ICheckout
{
    bool Scan( string item );
    int GetTotalPrice();
}

public class Checkout : ICheckout
{
    //Maybe an List<IGrouping> based on sku?
    private readonly List<Item> _basket = new List<Item>();

    private readonly IDictionary<string, int> _itemPrices;
    private readonly Dictionary<string, IPricingStrategy> _itemPricingStrategy;

    public Checkout( IDictionary<string, int> itemPrices, 
                        Dictionary<string, IPricingStrategy> itemPricingStrategy )
    {
        _itemPrices = itemPrices;
        _itemPricingStrategy = itemPricingStrategy;
    }

    public int GetTotalPrice()
    {
        return _basket.GroupBy( x => x.Sku )
                        .Select( x => GetPricingStrategy( x.Key )
                        .GetTotalPrice( x ) ).Sum();
    }

    public bool Scan( string item )
    {
        if ( _itemPrices.ContainsKey( item ) == false )
        {
            return false;
        }
        _basket.Add(new Item(item, _itemPrices[item] ) );

        return true;
    }

    private IPricingStrategy GetPricingStrategy( string item )
    {
        if ( _itemPricingStrategy.ContainsKey( item ) )
        {
            return _itemPricingStrategy[item];
        }
        return new ItemPriceStrategy();
    }
}
