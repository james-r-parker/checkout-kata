namespace Checkout;

public record Product(string Sku, ProductPrice Pricing);

// TODO: Confirm with team if a product price can have multiple offer prices. If so the offer should be a collection.
public record ProductPrice(decimal Price, ProductPriceOffer? Offer = null);
public record ProductPriceOffer(int Quantity, decimal Price);