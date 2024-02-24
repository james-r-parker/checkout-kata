using BenchmarkDotNet.Attributes;
using Checkout.Implementations;

namespace Checkout.Benchmark;

public class BasketTests
{
    private Basket _sut;

    [GlobalSetup]
    public void Setup()
    {
        _sut = new Basket(new InMemoryPricingRepository());
    }

    [Benchmark]
    public void Scan() => _sut.Scan("A");
}