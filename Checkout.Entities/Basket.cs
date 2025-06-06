using System.Collections.Immutable;

namespace Checkout.Entities;

public record Basket(ImmutableList<Item> Items, int CurrentTotal);

//add comment to see if I can commit