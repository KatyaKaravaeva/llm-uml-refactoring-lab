using BadDesignApp.Services.Payment;
using Xunit;

namespace BadDesignApp.Tests;

// Устранено нарушение Anx6 - модульные тесты для платежного сервиса
// Тесты проверяют только публичное поведение
public class PaymentServiceTests
{
    [Fact]
    public void ProcessPayment_ValidRequest_ShouldReturnSuccess()
    {
        // Arrange
        var service = new PaymentService();
        var request = new PaymentRequest { OrderId = "order123", Amount = 100m };
        
        // Act
        var result = service.ProcessPayment(request);
        
        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);
    }
    
    [Fact]
    public void ProcessPayment_NullRequest_ShouldReturnFailure()
    {
        // Arrange
        var service = new PaymentService();
        
        // Act
        var result = service.ProcessPayment(null!);
        
        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotNull(result.ErrorMessage);
    }
    
    [Fact]
    public void ProcessPayment_EmptyOrderId_ShouldReturnFailure()
    {
        // Arrange
        var service = new PaymentService();
        var request = new PaymentRequest { OrderId = "", Amount = 100m };
        
        // Act
        var result = service.ProcessPayment(request);
        
        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotNull(result.ErrorMessage);
    }
    
    [Fact]
    public void ProcessPayment_ZeroAmount_ShouldReturnFailure()
    {
        // Arrange
        var service = new PaymentService();
        var request = new PaymentRequest { OrderId = "order123", Amount = 0 };
        
        // Act
        var result = service.ProcessPayment(request);
        
        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotNull(result.ErrorMessage);
    }
}

