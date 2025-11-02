using BadDesignApp.Services.Business;
using BadDesignApp.Services.Data;
using BadDesignApp.Services.Output;
using BadDesignApp.Services.Validation;

namespace BadDesignApp.Services;

// Устранено нарушение SRP - оркестратор, координирующий специализированные сервисы
// Следует принципу Single Responsibility - только координация процесса заказа
public class OrderService
{
    private readonly IUserValidator _userValidator;
    private readonly IDiscountCalculator _discountCalculator;
    private readonly IOrderRepository _orderRepository;
    private readonly IOutputService _outputService;
    
    // Dependency Injection - устранена высокая связность
    public OrderService(
        IUserValidator userValidator,
        IDiscountCalculator discountCalculator,
        IOrderRepository orderRepository,
        IOutputService outputService)
    {
        _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
        _discountCalculator = discountCalculator ?? throw new ArgumentNullException(nameof(discountCalculator));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _outputService = outputService ?? throw new ArgumentNullException(nameof(outputService));
    }
    
    public OrderProcessingResult ProcessUserOrder(string userId, decimal amount, string userType)
    {
        // Валидация через отдельный сервис
        var validationResult = _userValidator.Validate(userId);
        if (!validationResult.IsValid)
        {
            _outputService.PrintError(validationResult.ErrorMessage ?? "Invalid user");
            return new OrderProcessingResult
            {
                Success = false,
                ErrorMessage = validationResult.ErrorMessage
            };
        }
        
        // Расчет скидки через отдельный сервис
        var discountInfo = _discountCalculator.CalculateDiscount(amount, userType);
        
        // Сохранение данных через отдельный сервис
        _orderRepository.SaveOrder(new OrderData
        {
            UserId = userId,
            Amount = discountInfo.FinalAmount
        });
        
        // Логирование через отдельный сервис
        var transactionCount = _orderRepository.GetTransactionCount();
        _outputService.Log($"{DateTime.Now}: User {userId} processed order {transactionCount}");
        
        // Вывод через отдельный сервис
        _outputService.PrintReceipt(new ReceiptData
        {
            UserId = userId,
            UserType = userType,
            Amount = discountInfo.FinalAmount,
            DiscountPercentage = discountInfo.DiscountPercentage
        });
        
        return new OrderProcessingResult
        {
            Success = true,
            FinalAmount = discountInfo.FinalAmount,
            DiscountPercentage = discountInfo.DiscountPercentage
        };
    }
}

public class OrderProcessingResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public decimal FinalAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
}

