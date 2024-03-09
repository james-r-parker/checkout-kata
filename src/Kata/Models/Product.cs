namespace Kata.Models;

/// <summary>
/// Details of the packaging available during checkout.
/// </summary>
/// <param name="Capacity">The number of items this packaging can store.</param>
/// <param name="Price">The price of the this packaging.</param>
public record BasketPackaging(int Capacity, decimal Price);

/// <summary>
/// Details of a product in the basket.
/// </summary>
/// <param name="Sku">The Sku of the produce.</param>
/// <param name="Quantity">The quantity of that item within the basket.</param>
public record BasketItem(string Sku, int Quantity);

/// <summary>
/// Details of the product handled by the checkout.
/// </summary>
/// <param name="Sku">SKU of the product, this is global unique per product.</param>
/// <param name="Pricing">Price details of the product.</param>
public record Product(string Sku, ProductPrice Pricing);

/// <summary>
/// Details of the product price.
/// </summary>
/// <param name="Price">The price GBP of the product.</param>
/// <param name="Offer">Details of the offer price if any for the product.</param>
public record ProductPrice(decimal Price, ProductPriceOffer? Offer = null);

/// <summary>
/// Details of the offer price of a product.
/// </summary>
/// <param name="Quantity">The quantity required to purchase to get the offer price.</param>
/// <param name="Price">The price in GBP the product can be purchased for if the offer quantity is satisfied.</param>
public record ProductPriceOffer(int Quantity, decimal Price);