namespace Checkout;

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
// TODO: Confirm with team if a products price needs to support multiple currencies.
// TODO: Confirm with team if a product price can have multiple offer prices. If so the offer should be a collection.
public record ProductPrice(decimal Price, ProductPriceOffer? Offer = null);

/// <summary>
/// Details of the offer price of a product.
/// </summary>
/// <param name="Quantity">The quantity required to purchase to get the offer price.</param>
/// <param name="Price">The price in GBP the product can be purchased for if the offer quantity is satisfied.</param>
public record ProductPriceOffer(int Quantity, decimal Price);