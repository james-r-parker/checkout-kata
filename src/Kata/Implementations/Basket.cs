using System.Collections.Concurrent;
using System.Collections.Frozen;
using Kata.Exceptions;
using Kata.Models;

namespace Kata.Implementations;

internal class Basket : IBasket
{
    private readonly ConcurrentDictionary<string, int> _basket = [];
    
    public IReadOnlyCollection<BasketItem> Items => 
        _basket
            .OrderBy(x => x.Key)
            .Select(x => new BasketItem(x.Key, x.Value))
            .ToFrozenSet();
    
    public void Add(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new BasketArgumentException("SKU cannot be null or empty", nameof(sku));
        }

        _basket.AddOrUpdate(sku, 1, (key, oldValue) => oldValue + 1);
    }

    public void Remove(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new BasketArgumentException("SKU cannot be null or empty", nameof(sku));
        }
        
        if (!_basket.TryRemove(sku, out _))
        {
            throw new BasketRemovalException();
        }
    }

    public void Clear()
    {
        _basket.Clear();
    }

    public void Update(string sku, int quantity)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new BasketArgumentException("SKU cannot be null or empty", nameof(sku));
        }
        
        if (quantity < 0)
        {
            throw new BasketArgumentException("Quantity must be greater than or equal to 0", nameof(quantity));
        }
        
        if (quantity is 0)
        {
            Remove(sku);
            return;
        }

        _basket.AddOrUpdate(sku, quantity, (key, oldValue) => quantity);
    }
}