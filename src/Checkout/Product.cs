namespace Checkout;

public record Product(string Sku, decimal Price, ProduceOffer? Offer = null);
public record ProduceOffer(int Quantity, decimal Price);