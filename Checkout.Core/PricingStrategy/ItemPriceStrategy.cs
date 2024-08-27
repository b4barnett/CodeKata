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
}

public class ItemPriceStrategy : IPricingStrategy
{
    public int GetTotalPrice( IGrouping<string, Item> basket ) => basket.Sum( x => x.Cost );
}
