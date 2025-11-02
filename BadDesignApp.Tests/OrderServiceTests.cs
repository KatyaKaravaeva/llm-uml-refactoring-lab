using BadDesignApp.Services;
using BadDesignApp.Services.Business;
using BadDesignApp.Services.Data;
using BadDesignApp.Services.Output;
using BadDesignApp.Services.Validation;
using Moq;
using Xunit;

namespace BadDesignApp.Tests;

// Устранено нарушение Anx1 - модульные тесты для каждого компонента
// Тесты изолированы через моки, проверяют только публичное поведение
public class OrderServiceTests
{
    // AAA паттерн (Arrange-Act-Assert) - лучшие практики тестирования
    [Fact]
    public void ProcessUserOrder_ValidUser_ShouldProcessSuccessfully()
    {
        // Arrange - подготовка: создание моков и настройка зависимостей
        var userValidatorMock = new Mock<IUserValidator>();
        userValidatorMock.Setup(v => v.Validate(It.IsAny<string>()))
            .Returns(ValidationResult.Success());
        
        var discountCalculatorMock = new Mock<IDiscountCalculator>();
        discountCalculatorMock.Setup(c => c.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new DiscountInfo
            {
                OriginalAmount = 100m,
                DiscountPercentage = 15m,
                DiscountAmount = 15m,
                FinalAmount = 85m
            });
        
        var orderRepositoryMock = new Mock<IOrderRepository>();
        orderRepositoryMock.Setup(r => r.GetTransactionCount()).Returns(1);
        
        var outputServiceMock = new Mock<IOutputService>();
        
        var orderService = new OrderService(
            userValidatorMock.Object,
            discountCalculatorMock.Object,
            orderRepositoryMock.Object,
            outputServiceMock.Object);
        
        // Act - выполнение: вызов тестируемого метода
        var result = orderService.ProcessUserOrder("user12345", 100m, "VIP");
        
        // Assert - проверка: проверка результата и взаимодействий
        Assert.True(result.Success);
        Assert.Equal(85m, result.FinalAmount);
        Assert.Equal(15m, result.DiscountPercentage);
        
        // Проверка взаимодействий через моки (не внутреннее состояние)
        userValidatorMock.Verify(v => v.Validate("user12345"), Times.Once);
        discountCalculatorMock.Verify(c => c.CalculateDiscount(100m, "VIP"), Times.Once);
        orderRepositoryMock.Verify(r => r.SaveOrder(It.IsAny<OrderData>()), Times.Once);
        outputServiceMock.Verify(o => o.PrintReceipt(It.IsAny<ReceiptData>()), Times.Once);
    }
    
    [Fact]
    public void ProcessUserOrder_InvalidUser_ShouldReturnFailure()
    {
        // Arrange
        var userValidatorMock = new Mock<IUserValidator>();
        userValidatorMock.Setup(v => v.Validate(It.IsAny<string>()))
            .Returns(ValidationResult.Failure("Invalid user ID"));
        
        var discountCalculatorMock = new Mock<IDiscountCalculator>();
        var orderRepositoryMock = new Mock<IOrderRepository>();
        var outputServiceMock = new Mock<IOutputService>();
        
        var orderService = new OrderService(
            userValidatorMock.Object,
            discountCalculatorMock.Object,
            orderRepositoryMock.Object,
            outputServiceMock.Object);
        
        // Act
        var result = orderService.ProcessUserOrder("abc", 100m, "VIP");
        
        // Assert
        Assert.False(result.Success);
        Assert.NotNull(result.ErrorMessage);
        
        // Проверка, что калькулятор и репозиторий не вызывались
        discountCalculatorMock.Verify(c => c.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()), Times.Never);
        orderRepositoryMock.Verify(r => r.SaveOrder(It.IsAny<OrderData>()), Times.Never);
        outputServiceMock.Verify(o => o.PrintError(It.IsAny<string>()), Times.Once);
    }
}

