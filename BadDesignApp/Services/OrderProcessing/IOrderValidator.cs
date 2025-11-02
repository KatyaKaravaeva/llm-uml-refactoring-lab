namespace BadDesignApp.Services.OrderProcessing;

// Устранено нарушение Anx2 - выделен интерфейс валидатора заказов
// Изменения в логике валидации не требуют правок в других методах
public interface IOrderValidator
{
    OrderValidationResult Validate(string orderId);
}

public class OrderValidationResult
{
    public bool IsValid { get; set; }
    public string? ErrorMessage { get; set; }
    
    public static OrderValidationResult Success() => new OrderValidationResult { IsValid = true };
    public static OrderValidationResult Failure(string errorMessage) => 
        new OrderValidationResult { IsValid = false, ErrorMessage = errorMessage };
}

