namespace BadDesignApp.Services.Payment;

// Устранено нарушение инкапсуляции - интерфейс для работы с платежами
// Устранена высокая связность - используется интерфейс вместо прямого обращения к полям
public interface IPaymentService
{
    PaymentResult ProcessPayment(PaymentRequest request);
}

public class PaymentRequest
{
    public string OrderId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

public class PaymentResult
{
    public bool IsSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    
    public static PaymentResult Success() => new PaymentResult { IsSuccessful = true };
    public static PaymentResult Failure(string errorMessage) => 
        new PaymentResult { IsSuccessful = false, ErrorMessage = errorMessage };
}

