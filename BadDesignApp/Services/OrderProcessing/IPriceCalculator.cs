namespace BadDesignApp.Services.OrderProcessing;

// Устранено нарушение Anx2 - выделен интерфейс для расчета цены
// Изменения в логике расчета не требуют правок в других методах
public interface IPriceCalculator
{
    PriceCalculationResult CalculatePrice(string orderId);
}

public class PriceCalculationResult
{
    public decimal Price { get; set; }
    public int ItemCount { get; set; }
}

