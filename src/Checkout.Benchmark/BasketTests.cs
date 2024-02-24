using BenchmarkDotNet.Attributes;
using Checkout.Implementations;

namespace Checkout.Benchmark;

public class BasketTests
{
    private readonly IEnumerable<Product> _products = new List<Product>()
    {
        new Product("A", new ProductPrice(50, new ProduceOffer(3,130))),
        new Product("B", new ProductPrice(30, new ProduceOffer(2,45))),
        new Product("C", new ProductPrice(20)),
        new Product("D", new ProductPrice(15)),
    };
    
    private Basket _sut;

    [GlobalSetup]
    public void Setup()
    {
        _sut = new Basket(new InMemoryPricingRepository(_products));
    }

    [Benchmark]
    public void Scan() => _sut.Scan("A");
}