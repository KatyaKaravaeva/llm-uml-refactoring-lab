namespace BadDesignApp.Services.Payment;

// Устранено нарушение инкапсуляции - свойства вместо публичных полей
// Устранена высокая связность - класс инкапсулирует свою логику
public class PaymentService : IPaymentService
{
    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        if (request == null)
            return PaymentResult.Failure("Payment request cannot be null");
        
        if (string.IsNullOrEmpty(request.OrderId))
            return PaymentResult.Failure("Order ID is required");
        
        if (request.Amount <= 0)
            return PaymentResult.Failure("Amount must be greater than zero");
        
        // Эмуляция обработки платежа
        Console.WriteLine($"Processing payment {request.Amount:C} for order {request.OrderId}");
        
        return PaymentResult.Success();
    }
}

