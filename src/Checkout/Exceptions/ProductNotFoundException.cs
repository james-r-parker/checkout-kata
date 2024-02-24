namespace Checkout.Exceptions;

public class ProductNotFoundException(string sku) : Exception($"Product with SKU {sku} not found");