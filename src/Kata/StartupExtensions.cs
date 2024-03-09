using Kata.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kata;

public static class StartupExtensions
{
    public static IServiceCollection AddKata(this IServiceCollection services)
    {
        services.TryAddScoped<IBasket, Basket>();
        services.TryAddSingleton<IPackagingCalculator, QuantityBasedPackagingCalculator>();
        services.TryAddSingleton<ICheckout, DiscountedPackagedCheckout>();
        return services;
    }
}