using BadDesignApp.Services;
using BadDesignApp.Services.Business;
using BadDesignApp.Services.Data;
using BadDesignApp.Services.OrderProcessing;
using BadDesignApp.Services.Output;
using BadDesignApp.Services.Payment;
using BadDesignApp.Services.Shapes;
using BadDesignApp.Services.Validation;

namespace BadDesignApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Демонстрация отрефакторенного приложения ===\n");
        
        // Устранено нарушение Anx1 - использование Dependency Injection
        // Создание специализированных сервисов вместо GodClass
        var userValidator = new UserValidator();
        var discountCalculator = new DiscountCalculator();
        var orderRepository = new OrderRepository();
        var outputService = new ConsoleOutputService();
        
        var orderService = new OrderService(
            userValidator,
            discountCalculator,
            orderRepository,
            outputService);
        
        Console.WriteLine("1. Обработка заказа пользователя:");
        var result = orderService.ProcessUserOrder("user12345", 100.50m, "VIP");
        Console.WriteLine($"Результат: {(result.Success ? "Успешно" : "Ошибка")}\n");
        
        // Устранено нарушение Anx2 и Anx6 - использование рефакторенного OrderProcessor
        var orderValidator = new OrderValidator();
        var priceCalculator = new PriceCalculator();
        var paymentService = new PaymentService();
        
        var refactoredOrderProcessor = new RefactoredOrderProcessor(
            orderValidator,
            priceCalculator,
            paymentService,
            outputService);
        
        Console.WriteLine("2. Обработка заказа через рефакторенный процессор:");
        var processingResult = refactoredOrderProcessor.Process("order456", "user123");
        Console.WriteLine($"Результат: {(processingResult.Success ? "Успешно" : "Ошибка")}\n");
        
        // Устранено нарушение Anx3 - единая иерархия фигур
        Console.WriteLine("3. Демонстрация единой иерархии фигур:");
        var circle = new Circle { Radius = 5, Color = "Red" };
        var rectangle = new Rectangle { Width = 4, Height = 5, Color = "Blue" };
        
        Console.WriteLine($"Площадь круга: {circle.GetArea():F2}");
        circle.Draw();
        
        Console.WriteLine($"Площадь прямоугольника: {rectangle.GetArea():F2}");
        rectangle.Draw();
        Console.WriteLine();
        
        // Устранено нарушение Anx6 - использование рефакторенного DataAccessor
        Console.WriteLine("4. Демонстрация правильной инкапсуляции:");
        var dataAccessor = new Services.Data.RefactoredDataAccessor();
        dataAccessor.Data = "test data";
        dataAccessor.Initialize();
        dataAccessor.IncrementCounter();
        
        Console.WriteLine($"Данные: {dataAccessor.Data}");
        Console.WriteLine($"Счетчик: {dataAccessor.Counter}");
        Console.WriteLine($"Инициализирован: {dataAccessor.IsInitialized}");
        
        Console.WriteLine("\n=== Приложение выполнено с улучшенной архитектурой! ===");
    }
}
