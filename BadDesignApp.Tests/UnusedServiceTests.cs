using BadDesignApp.Services;
using Xunit;

namespace BadDesignApp.Tests;

// Anx4: Тесты для ленивого класса, который почти не используется
// Бесполезные тесты без практического смысла
public class UnusedServiceTests
{
    [Fact]
    public void DoSomething_ShouldIncrementCallCount()
    {
        // Тестируем класс, который не используется в реальном коде
        var service = new UnusedService();
        var initialCount = service.GetCallCount();
        
        service.DoSomething();
        
        var newCount = service.GetCallCount();
        Assert.Equal(initialCount + 1, newCount);
    }
    
    [Fact]
    public void ProcessUnusedData_ShouldReturnProcessedString()
    {
        // Бесполезный тест для неиспользуемого метода
        var service = new UnusedService();
        var result = service.ProcessUnusedData("test");
        
        // Тест дублирует логику реализации
        Assert.Equal("Processed: test", result);
    }
    
    [Fact]
    public void GetCallCount_ShouldReturnZeroInitially()
    {
        // Тривиальный тест для неиспользуемого класса
        var service = new UnusedService();
        Assert.Equal(0, service.GetCallCount());
    }
}


