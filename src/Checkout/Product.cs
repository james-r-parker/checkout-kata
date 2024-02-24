namespace Checkout;

public record Product(string Sku, ProductPrice Pricing);
public record ProductPrice(decimal Price, ProduceOffer? Offer = null);
public record ProduceOffer(int Quantity, decimal Price);