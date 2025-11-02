using BadDesignApp.Services;

namespace BadDesignApp;

class Program
{
    static void Main(string[] args)
    {
        // Демонстрация плохого дизайна
        var godClass = new GodClassService();
        godClass.ProcessUserOrder("user123", 100.50m, "VIP");
        
        var orderProcessor = new OrderProcessor();
        orderProcessor.Process("order456", "user123");
        
        var unusedService = new UnusedService();
        unusedService.DoSomething();
        
        Console.WriteLine("Приложение выполнено с плохим дизайном!");
    }
}


