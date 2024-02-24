namespace Checkout.Exceptions;

public class BasketArgumentException(string? message, string? paramName) : ArgumentException(message, paramName);