using Kata.Exceptions;

namespace Kata.Tests.Basket;

public class BasketTests
{
    private readonly IBasket _sut;

    public BasketTests()
    {
        _sut = new Implementations.Basket();
    }
    
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [Theory]
    public void Add_Throws_Exception_When_Sku_Is_Null_Or_Empty(string sku)
    {
        var ex = Assert.Throws<BasketArgumentException>(() => _sut.Add(sku));
        Assert.Equal("SKU cannot be null or empty (Parameter 'sku')", ex.Message);
    }
    
    [Fact]
    public void Add_Single_Item()
    {
        _sut.Add("A");
        Assert.Collection(_sut.Items, 
            item => Assert.Equal("A", item.Sku));
    }

    [Fact]
    public void Add_Two_Different_Items()
    {
        _sut.Add("A");
        _sut.Add("B");
        
        Assert.Collection(_sut.Items, 
            item =>
            {
                Assert.Equal("A", item.Sku);
                Assert.Equal(1, item.Quantity);
            },
            item =>
            {
                Assert.Equal("B", item.Sku);
                Assert.Equal(1, item.Quantity);
            });
    }

    [Fact]
    public void Add_Two_Identical_Items()
    {
        _sut.Add("A");
        _sut.Add("A");

        Assert.Collection(_sut.Items, 
            item =>
            {
                Assert.Equal("A", item.Sku);
                Assert.Equal(2, item.Quantity);
            });
    }

    [Fact]
    public void Update_Single_Item_Does_Not_Exist_Already()
    {
        _sut.Update("A", 2);
        Assert.Collection(_sut.Items, 
            item =>
            {
                Assert.Equal("A", item.Sku);
                Assert.Equal(2, item.Quantity);
            });
    }
    
    [Fact]
    public void Update_Single_Item_Does_Already_Exists()
    {
        _sut.Add("A");
        _sut.Update("A", 10);
        Assert.Collection(_sut.Items, 
            item =>
            {
                Assert.Equal("A", item.Sku);
                Assert.Equal(10, item.Quantity);
            });
    }
    
    [Fact]
    public void Update_Removes_Existing_Item_With_Zero_Quantity()
    {
        _sut.Add("A");
        _sut.Update("A", 0);
        Assert.Empty(_sut.Items);
    }

    [Fact]
    public void Update_Product_Invalid_Quantity_Throws()
    {
        var ex = Assert.Throws<BasketArgumentException>(() => _sut.Update("A", -1));
        Assert.Equal("Quantity must be greater than or equal to 0 (Parameter 'quantity')", ex.Message);
    }
    
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [Theory]
    public void Update_Throws_Exception_When_Sku_Is_Null_Or_Empty(string sku)
    {
        var ex = Assert.Throws<BasketArgumentException>(() => _sut.Update(sku, 1));
        Assert.Equal("SKU cannot be null or empty (Parameter 'sku')", ex.Message);
    }

    [Fact]
    public void Update_To_Zero_Does_Not_Exist_Throws()
    {
        var ex = Assert.Throws<BasketRemovalException>(() => _sut.Update("A", 0));
        Assert.Equal("Failed to remove product from the basket", ex.Message);
    }

    [Fact]
    public void Remove_Existing_Item()
    {
        _sut.Add("A");
        _sut.Remove("A");
        Assert.Empty(_sut.Items);
    }
    
    [Fact]
    public void Remove_Does_Not_Exist_Throws()
    {
        var ex = Assert.Throws<BasketRemovalException>(() => _sut.Remove("A"));
        Assert.Equal("Failed to remove product from the basket", ex.Message);
    }
    
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [Theory]
    public void Remove_Throws_Exception_When_Sku_Is_Null_Or_Empty(string sku)
    {
        var ex = Assert.Throws<BasketArgumentException>(() => _sut.Remove(sku));
        Assert.Equal("SKU cannot be null or empty (Parameter 'sku')", ex.Message);
    }

    [Fact]
    public void Clear()
    {
        _sut.Add("A");
        _sut.Clear();
        Assert.Empty(_sut.Items);
    }
}