using BadDesignApp.Services.OrderProcessing;
using BadDesignApp.Services.Output;
using BadDesignApp.Services.Payment;
using Moq;
using Xunit;

namespace BadDesignApp.Tests;

// Устранено нарушение Anx2 - независимые тесты, не ломаются при малых изменениях
// Устранено нарушение Anx6 - тесты используют моки, не обращаются к приватным полям
// Высокая изоляция - каждый тест проверяет только одну логическую часть поведения
public class RefactoredOrderProcessorTests
{
    [Fact]
    public void Process_ValidOrder_ShouldProcessSuccessfully()
    {
        // Arrange - настройка всех зависимостей через моки
        var orderValidatorMock = new Mock<IOrderValidator>();
        orderValidatorMock.Setup(v => v.Validate(It.IsAny<string>()))
            .Returns(OrderValidationResult.Success());
        
        var priceCalculatorMock = new Mock<IPriceCalculator>();
        priceCalculatorMock.Setup(c => c.CalculatePrice(It.IsAny<string>()))
            .Returns(new PriceCalculationResult { ItemCount = 8, Price = 84m });
        
        var paymentServiceMock = new Mock<IPaymentService>();
        paymentServiceMock.Setup(p => p.ProcessPayment(It.IsAny<PaymentRequest>()))
            .Returns(PaymentResult.Success());
        
        var outputServiceMock = new Mock<IOutputService>();
        
        var processor = new RefactoredOrderProcessor(
            orderValidatorMock.Object,
            priceCalculatorMock.Object,
            paymentServiceMock.Object,
            outputServiceMock.Object);
        
        // Act
        var result = processor.Process("order123", "user456");
        
        // Assert - проверка только публичного поведения
        Assert.True(result.Success);
        Assert.Equal(OrderStatus.Processing, processor.Status);
        Assert.Equal("order123", processor.OrderId);
        Assert.Equal("user456", processor.UserId);
        Assert.Equal(84m, processor.Price);
        Assert.Equal(8, processor.ItemCount);
        
        // Проверка взаимодействий через моки
        orderValidatorMock.Verify(v => v.Validate("order123"), Times.Once);
        priceCalculatorMock.Verify(c => c.CalculatePrice("order123"), Times.Once);
        paymentServiceMock.Verify(p => p.ProcessPayment(It.Is<PaymentRequest>(
            r => r.OrderId == "order123" && r.Amount == 84m)), Times.Once);
    }
    
    [Fact]
    public void Process_InvalidOrderId_ShouldReturnFailure()
    {
        // Arrange
        var orderValidatorMock = new Mock<IOrderValidator>();
        orderValidatorMock.Setup(v => v.Validate(It.IsAny<string>()))
            .Returns(OrderValidationResult.Failure("Order ID cannot be empty"));
        
        var priceCalculatorMock = new Mock<IPriceCalculator>();
        var paymentServiceMock = new Mock<IPaymentService>();
        var outputServiceMock = new Mock<IOutputService>();
        
        var processor = new RefactoredOrderProcessor(
            orderValidatorMock.Object,
            priceCalculatorMock.Object,
            paymentServiceMock.Object,
            outputServiceMock.Object);
        
        // Act
        var result = processor.Process("", "user456");
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal(OrderStatus.Invalid, processor.Status);
        
        // Проверка, что калькулятор и платежный сервис не вызывались
        priceCalculatorMock.Verify(c => c.CalculatePrice(It.IsAny<string>()), Times.Never);
        paymentServiceMock.Verify(p => p.ProcessPayment(It.IsAny<PaymentRequest>()), Times.Never);
    }
    
    [Fact]
    public void Process_PaymentFails_ShouldReturnFailure()
    {
        // Arrange
        var orderValidatorMock = new Mock<IOrderValidator>();
        orderValidatorMock.Setup(v => v.Validate(It.IsAny<string>()))
            .Returns(OrderValidationResult.Success());
        
        var priceCalculatorMock = new Mock<IPriceCalculator>();
        priceCalculatorMock.Setup(c => c.CalculatePrice(It.IsAny<string>()))
            .Returns(new PriceCalculationResult { ItemCount = 8, Price = 84m });
        
        var paymentServiceMock = new Mock<IPaymentService>();
        paymentServiceMock.Setup(p => p.ProcessPayment(It.IsAny<PaymentRequest>()))
            .Returns(PaymentResult.Failure("Payment failed"));
        
        var outputServiceMock = new Mock<IOutputService>();
        
        var processor = new RefactoredOrderProcessor(
            orderValidatorMock.Object,
            priceCalculatorMock.Object,
            paymentServiceMock.Object,
            outputServiceMock.Object);
        
        // Act
        var result = processor.Process("order123", "user456");
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal(OrderStatus.Invalid, processor.Status);
        Assert.NotNull(result.ErrorMessage);
    }
}

