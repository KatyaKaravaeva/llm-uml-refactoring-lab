using BadDesignApp.Services.Output;
using BadDesignApp.Services.Payment;

namespace BadDesignApp.Services.OrderProcessing;

// Устранено нарушение Anx2 - методы независимы, изменения в одном не требуют правок в других
// Устранено нарушение Anx6 - инкапсуляция через свойства, нет прямого доступа к полям
// Устранена высокая связность - используются интерфейсы и Dependency Injection
public class RefactoredOrderProcessor
{
    // Инкапсуляция через свойства вместо публичных полей
    public string OrderId { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; }
    public decimal Price { get; private set; }
    public int ItemCount { get; private set; }
    
    private readonly IOrderValidator _orderValidator;
    private readonly IPriceCalculator _priceCalculator;
    private readonly IPaymentService _paymentService;
    private readonly IOutputService _outputService;
    
    // Dependency Injection - устранена жесткая связанность
    public RefactoredOrderProcessor(
        IOrderValidator orderValidator,
        IPriceCalculator priceCalculator,
        IPaymentService paymentService,
        IOutputService outputService)
    {
        _orderValidator = orderValidator ?? throw new ArgumentNullException(nameof(orderValidator));
        _priceCalculator = priceCalculator ?? throw new ArgumentNullException(nameof(priceCalculator));
        _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        _outputService = outputService ?? throw new ArgumentNullException(nameof(outputService));
    }
    
    // Устранено нарушение Anx2 - метод легко расширяется, независимые шаги
    public ProcessingResult Process(string orderId, string userId)
    {
        OrderId = orderId;
        UserId = userId;
        Status = OrderStatus.Pending;
        
        // Каждый шаг независим - изменения в одном не требуют правок в других
        var validationResult = _orderValidator.Validate(orderId);
        if (!validationResult.IsValid)
        {
            Status = OrderStatus.Invalid;
            _outputService.PrintError(validationResult.ErrorMessage ?? "Validation failed");
            return new ProcessingResult { Success = false, Status = Status };
        }
        
        var priceResult = _priceCalculator.CalculatePrice(orderId);
        Price = priceResult.Price;
        ItemCount = priceResult.ItemCount;
        
        var paymentResult = _paymentService.ProcessPayment(new PaymentRequest
        {
            OrderId = orderId,
            Amount = Price
        });
        
        if (!paymentResult.IsSuccessful)
        {
            Status = OrderStatus.Invalid;
            return new ProcessingResult { Success = false, Status = Status, ErrorMessage = paymentResult.ErrorMessage };
        }
        
        Status = OrderStatus.Paid;
        UpdateStatus();
        NotifyUser();
        
        return new ProcessingResult { Success = true, Status = Status };
    }
    
    private void UpdateStatus()
    {
        if (Status == OrderStatus.Paid)
            Status = OrderStatus.Processing;
    }
    
    private void NotifyUser()
    {
        _outputService.Log($"Order {OrderId} for user {UserId} is {Status}");
    }
}

public enum OrderStatus
{
    Pending,
    Invalid,
    Paid,
    Processing
}

public class ProcessingResult
{
    public bool Success { get; set; }
    public OrderStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
}

