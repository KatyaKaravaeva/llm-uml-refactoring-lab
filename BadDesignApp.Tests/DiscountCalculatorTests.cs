using BadDesignApp.Services.Business;
using Xunit;

namespace BadDesignApp.Tests;

// Устранено нарушение Anx1 - модульные тесты для калькулятора скидок
public class DiscountCalculatorTests
{
    [Fact]
    public void CalculateDiscount_VipUser_ShouldApplyVipDiscount()
    {
        // Arrange
        var calculator = new DiscountCalculator();
        
        // Act
        var result = calculator.CalculateDiscount(100m, "VIP");
        
        // Assert
        Assert.Equal(100m, result.OriginalAmount);
        Assert.Equal(15m, result.DiscountPercentage);
        Assert.Equal(15m, result.DiscountAmount);
        Assert.Equal(85m, result.FinalAmount);
    }
    
    [Fact]
    public void CalculateDiscount_PremiumUser_ShouldApplyPremiumDiscount()
    {
        // Arrange
        var calculator = new DiscountCalculator();
        
        // Act
        var result = calculator.CalculateDiscount(100m, "Premium");
        
        // Assert
        Assert.Equal(100m, result.OriginalAmount);
        Assert.Equal(10m, result.DiscountPercentage);
        Assert.Equal(10m, result.DiscountAmount);
        Assert.Equal(90m, result.FinalAmount);
    }
    
    [Fact]
    public void CalculateDiscount_RegularUser_ShouldApplyNoDiscount()
    {
        // Arrange
        var calculator = new DiscountCalculator();
        
        // Act
        var result = calculator.CalculateDiscount(100m, "Regular");
        
        // Assert
        Assert.Equal(100m, result.OriginalAmount);
        Assert.Equal(0m, result.DiscountPercentage);
        Assert.Equal(0m, result.DiscountAmount);
        Assert.Equal(100m, result.FinalAmount);
    }
}

