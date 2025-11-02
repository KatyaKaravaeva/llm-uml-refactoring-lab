using BadDesignApp.Services;
using Xunit;

namespace BadDesignApp.Tests;

// Anx2: Тесты чувствительны к внутренним деталям реализации
// Anx6: Тесты обращаются напрямую к полям, нарушая инкапсуляцию
// Высокая связность - тесты используют реальные зависимости без моков
public class OrderProcessorTests
{
    [Fact]
    public void Process_ValidOrder_ShouldUpdateStatus()
    {
        // Тест использует реальную зависимость без моков
        var processor = new OrderProcessor();
        processor.Process("order123", "user456");
        
        // Прямой доступ к публичным полям (нарушение инкапсуляции)
        Assert.Equal("order123", processor.OrderId);
        Assert.Equal("user456", processor.UserId);
        Assert.Equal(OrderStatus.Processing, processor.Status);
        
        // Тест знает внутреннюю логику расчета цены
        Assert.True(processor.Price > 0);
        Assert.True(processor.ItemCount > 0);
    }
    
    [Fact]
    public void Process_InvalidOrder_ShouldSetInvalidStatus()
    {
        // Тест зависит от внутренней логики валидации
        var processor = new OrderProcessor();
        processor.Process("", "user456"); // Пустой OrderId
        
        // Прямой доступ к полям
        Assert.Equal(OrderStatus.Invalid, processor.Status);
        Assert.Equal("", processor.OrderId);
    }
    
    [Fact]
    public void Process_ShouldInteractWithPaymentProcessor()
    {
        // Тест проверяет взаимодействие между классами
        // Высокая связность - тест знает о внутренней структуре
        var processor = new OrderProcessor();
        processor.Process("order123", "user456");
        
        // Тест знает, что внутри используется PaymentProcessor
        // и что цена устанавливается через прямое обращение к полям
        Assert.True(processor.Price > 0);
        
        // Тест чувствителен к изменению в методе CalculatePrice
        // Если изменится логика расчета, тест сломается
        var expectedPrice = "order123".Length * 10.5m;
        Assert.Equal(expectedPrice, processor.Price);
    }
    
    // Anx2: При изменении логики Process нужно обновить множество тестов
    [Fact]
    public void Process_ShouldCalculateItemCountBasedOnOrderIdLength()
    {
        // Тест дублирует логику реализации
        var processor = new OrderProcessor();
        processor.Process("order123", "user456");
        
        // Тест знает, что ItemCount = OrderId.Length
        Assert.Equal("order123".Length, processor.ItemCount);
    }
}


