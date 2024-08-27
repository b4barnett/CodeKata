using System.Collections.Immutable;

namespace Checkout.Entities;

public record Basket(ImmutableList<Item> Items, int CurrentTotal);