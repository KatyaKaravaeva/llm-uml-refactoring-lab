namespace BadDesignApp.Services;

// Anx2: Расходящиеся модификации - изменение логики требует редактирования многих методов
// Anx6: Нарушение инкапсуляции - прямой доступ к полям
// Высокая связность - классы тесно взаимодействуют
public class OrderProcessor
{
    // Публичные поля - нарушение инкапсуляции
    public string OrderId;
    public string UserId;
    public OrderStatus Status;
    public decimal Price;
    public int ItemCount;
    
    // Прямое взаимодействие с другим классом
    private readonly PaymentProcessor _paymentProcessor;
    
    public OrderProcessor()
    {
        _paymentProcessor = new PaymentProcessor();
    }
    
    // Anx2: Изменение логики обработки требует изменения множества методов
    public void Process(string orderId, string userId)
    {
        OrderId = orderId;
        UserId = userId;
        Status = OrderStatus.Pending;
        
        // Прямое изменение состояния другого объекта
        _paymentProcessor.CurrentOrderId = orderId;
        _paymentProcessor.Amount = 0; // Инициализация
        
        ValidateOrder();
        CalculatePrice();
        ProcessPayment();
        UpdateStatus();
        NotifyUser();
    }
    
    // Anx2: При изменении валидации нужно менять этот метод и Process
    private void ValidateOrder()
    {
        if (string.IsNullOrEmpty(OrderId))
        {
            Status = OrderStatus.Invalid;
            // Прямое изменение другого класса
            _paymentProcessor.IsValid = false;
            return;
        }
        _paymentProcessor.IsValid = true;
    }
    
    // Anx2: При изменении расчета цены нужно менять этот метод и Process
    private void CalculatePrice()
    {
        ItemCount = OrderId.Length; // Плохая логика для демонстрации
        Price = ItemCount * 10.5m;
        
        // Прямое изменение другого класса
        _paymentProcessor.Amount = Price;
    }
    
    // Anx2: При изменении оплаты нужно менять этот метод и Process
    private void ProcessPayment()
    {
        // Прямое обращение к полям другого класса
        if (_paymentProcessor.IsValid && _paymentProcessor.Amount > 0)
        {
            _paymentProcessor.Process();
            Status = OrderStatus.Paid;
        }
    }
    
    private void UpdateStatus()
    {
        if (Status == OrderStatus.Paid)
            Status = OrderStatus.Processing;
    }
    
    private void NotifyUser()
    {
        Console.WriteLine($"Order {OrderId} for user {UserId} is {Status}");
    }
}

public enum OrderStatus
{
    Pending,
    Invalid,
    Paid,
    Processing
}

// Тесно связанный класс
public class PaymentProcessor
{
    // Публичные поля - нарушение инкапсуляции Anx6
    public string CurrentOrderId;
    public decimal Amount;
    public bool IsValid;
    
    // Прямой доступ к внутреннему полю другого класса
    public void Process()
    {
        // Слишком высокая связанность
        Console.WriteLine($"Processing payment {Amount:C} for order {CurrentOrderId}");
    }
}


