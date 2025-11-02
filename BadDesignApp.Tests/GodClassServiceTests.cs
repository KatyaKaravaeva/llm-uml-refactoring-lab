using BadDesignApp.Services;
using Xunit;

namespace BadDesignApp.Tests;

// Anx1: Монолитные тесты для GodClass
// Тесты зависят от внутреннего состояния и деталей реализации
public class GodClassServiceTests
{
    [Fact]
    public void ProcessUserOrder_ValidUser_ShouldProcessCorrectly()
    {
        // Тест зависит от внутреннего состояния класса
        var service = new GodClassService();
        service.ProcessUserOrder("user12345", 100.0m, "VIP");
        
        // Прямой доступ к внутреннему состоянию через геттеры
        var userId = service.GetCurrentUserId();
        var amount = service.GetTotalAmount();
        var isValidated = service.IsValidated();
        
        // Тест чувствителен к внутренним деталям реализации
        Assert.Equal("user12345", userId);
        Assert.True(amount < 100.0m); // Знаем про скидку VIP
        Assert.True(isValidated);
        
        // Ещё одна проверка внутреннего состояния
        var transactionCount = service.GetTransactionCount();
        Assert.Equal(1, transactionCount);
    }
    
    [Fact]
    public void ProcessUserOrder_InvalidUser_ShouldFail()
    {
        // Тест знает внутренние детали валидации
        var service = new GodClassService();
        service.ProcessUserOrder("abc", 100.0m, "VIP"); // Короткий ID
        
        // Тест зависит от внутренней логики валидации
        Assert.False(service.IsValidated());
        Assert.NotNull(service.GetLastError());
        
        // Тест проверяет, что транзакция не была обработана
        var transactionCount = service.GetTransactionCount();
        Assert.Equal(0, transactionCount); // Знаем внутреннюю логику
    }
    
    [Fact]
    public void ProcessUserOrder_DifferentUserTypes_ShouldApplyDifferentDiscounts()
    {
        // Тест проверяет внутреннюю логику расчета скидок
        var vipService = new GodClassService();
        vipService.ProcessUserOrder("user12345", 100.0m, "VIP");
        var vipAmount = vipService.GetTotalAmount();
        
        var premiumService = new GodClassService();
        premiumService.ProcessUserOrder("user12345", 100.0m, "Premium");
        var premiumAmount = premiumService.GetTotalAmount();
        
        var regularService = new GodClassService();
        regularService.ProcessUserOrder("user12345", 100.0m, "Regular");
        var regularAmount = regularService.GetTotalAmount();
        
        // Тест знает точные значения скидок (15%, 10%, 0%)
        Assert.Equal(85.0m, vipAmount);
        Assert.Equal(90.0m, premiumAmount);
        Assert.Equal(100.0m, regularAmount);
    }
    
    // Монолитный тест, проверяющий множество аспектов
    [Fact]
    public void ProcessUserOrder_FullFlow_ShouldWork()
    {
        var service = new GodClassService();
        service.ProcessUserOrder("user12345", 200.0m, "VIP");
        
        // Проверяем все внутренние состояния
        Assert.Equal("user12345", service.GetCurrentUserId());
        Assert.True(service.GetTotalAmount() > 0);
        Assert.True(service.IsValidated());
        Assert.Equal(1, service.GetTransactionCount());
        Assert.Null(service.GetLastError());
    }
}


