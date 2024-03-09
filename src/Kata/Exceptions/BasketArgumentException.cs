namespace Kata.Exceptions;

public class BasketArgumentException(string? message, string? paramName) : ArgumentException(message, paramName);