namespace BadDesignApp.Services.OrderProcessing;

// Устранено нарушение Anx2 - отдельный класс валидации заказов
public class OrderValidator : IOrderValidator
{
    public OrderValidationResult Validate(string orderId)
    {
        if (string.IsNullOrEmpty(orderId))
        {
            return OrderValidationResult.Failure("Order ID cannot be empty");
        }
        
        return OrderValidationResult.Success();
    }
}

