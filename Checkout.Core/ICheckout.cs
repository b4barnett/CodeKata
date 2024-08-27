using Checkout.Entities;

namespace Checkout.Core;

public interface ICheckout
{
    void Scan( string item );
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
        throw new NotImplementedException();
    }

    public void Scan( string item )
    {
        _basket.Add(new Item(item, _itemPrices[item]));
    }
}
