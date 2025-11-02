namespace BadDesignApp.Services;

// Anx4: Ленивый класс - почти не используется, но тестируется отдельно
public class UnusedService
{
    private int _callCount;
    
    public UnusedService()
    {
        _callCount = 0;
    }
    
    // Метод почти никогда не вызывается в реальном коде
    public void DoSomething()
    {
        _callCount++;
        Console.WriteLine($"UnusedService called {_callCount} times");
    }
    
    // Методы для тестов, хотя класс не используется
    public int GetCallCount() => _callCount;
    
    public string ProcessUnusedData(string data)
    {
        // Бесполезная логика для демонстрации
        return $"Processed: {data}";
    }
}


