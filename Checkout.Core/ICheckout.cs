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
    private List<Item> _basket = new List<Item>();

    private IDictionary<string, int> _itemPrices;

    public Checkout( IDictionary<string, int> itemPrices )
    {
        _itemPrices = itemPrices;
    }

    public int GetTotalPrice()
    {
        return _basket.Select(x => x.Cost).Sum();
    }

    public bool Scan( string item )
    {
        if ( _itemPrices.ContainsKey( item ) == false )
        {
            return false;
        }
        _basket.Add(new Item(item, _itemPrices[item]));

        return true;
    }
}
