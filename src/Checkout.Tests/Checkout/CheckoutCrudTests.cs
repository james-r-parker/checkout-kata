using Checkout.Exceptions;
using Checkout.Implementations;

namespace Checkout.Tests.Checkout;

public class CheckoutCrudTests
{
    private readonly IEnumerable<Product> _products = new List<Product>()
    {
        new("A", new ProductPrice(5)),
        new("B", new ProductPrice(7)),
    };

    private readonly ICheckout _sut;

    public CheckoutCrudTests()
    {
        _sut = new Basket(new InMemoryPricingRepository(_products));
    }
    
    [Fact]
    public async Task Add_And_Clear()
    {
        _sut.Scan("A");
        _sut.Scan("A");
        _sut.Clear();
        _sut.Scan("B");

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(7, result);
    }
    
    [Fact]
    public async Task Add_And_Remove_Product()
    {
        _sut.Scan("A");
        _sut.Scan("B");
        _sut.Remove("A");

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(7, result);
    }
    
    [Fact]
    public async Task Add_And_Update_Product()
    {
        _sut.Scan("A");
        _sut.Scan("B");
        _sut.Update("A", 2);

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(17, result);
    }
    
    [Fact]
    public async Task Update_Product()
    {
        _sut.Update("A", 2);

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(10, result);
    }
    
    [Fact]
    public void Update_Product_Invalid_Quantity_Throws()
    {
        var ex = Assert.Throws<BasketArgumentException>(() => _sut.Update("A", -1));
        Assert.Equal("Quantity must be greater than or equal to 0 (Parameter 'quantity')", ex.Message);
    }
    
    [Fact]
    public async Task Add_And_Update_To_Zero_Product()
    {
        _sut.Scan("A");
        _sut.Scan("B");
        _sut.Update("A", 0);

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(7, result);
    }
    
    [Fact]
    public void Update_To_Zero_Does_Not_Exist_Throws()
    {
        var ex = Assert.Throws<BasketRemovalException>(() => _sut.Update("A", 0));
        Assert.Equal("Failed to remove product from the basket",ex.Message);
    }
    
    [Fact]
    public void Remove_Does_Not_Exist_Throws()
    {
        var ex = Assert.Throws<BasketRemovalException>(() => _sut.Remove("A"));
        Assert.Equal("Failed to remove product from the basket",ex.Message);
    }

    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [Theory]
    public void Scan_Throws_Exception_When_Sku_Is_Null_Or_Empty(string sku)
    {
        var ex = Assert.Throws<BasketArgumentException>(() => _sut.Scan(sku));
        Assert.Equal("SKU cannot be null or empty (Parameter 'sku')",ex.Message);
    }
    
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [Theory]
    public void Remove_Throws_Exception_When_Sku_Is_Null_Or_Empty(string sku)
    {
        var ex = Assert.Throws<BasketArgumentException>(() => _sut.Remove(sku));
        Assert.Equal("SKU cannot be null or empty (Parameter 'sku')",ex.Message);
    }
    
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [Theory]
    public void Update_Throws_Exception_When_Sku_Is_Null_Or_Empty(string sku)
    {
        var ex = Assert.Throws<BasketArgumentException>(() => _sut.Update(sku, 1));
        Assert.Equal("SKU cannot be null or empty (Parameter 'sku')",ex.Message);
    }
}