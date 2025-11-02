namespace BadDesignApp.Services.OrderProcessing;

// Устранено нарушение Anx2 - отдельный класс для расчета цены
public class PriceCalculator : IPriceCalculator
{
    private const decimal PricePerItem = 10.5m;
    
    public PriceCalculationResult CalculatePrice(string orderId)
    {
        // Логика расчета может быть легко изменена без влияния на другие компоненты
        var itemCount = orderId.Length;
        var price = itemCount * PricePerItem;
        
        return new PriceCalculationResult
        {
            ItemCount = itemCount,
            Price = price
        };
    }
}

