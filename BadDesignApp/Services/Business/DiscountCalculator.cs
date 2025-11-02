namespace BadDesignApp.Services.Business;

// Устранено нарушение SRP - отдельный класс для бизнес-логики расчета скидок
public interface IDiscountCalculator
{
    DiscountInfo CalculateDiscount(decimal amount, string userType);
}

public class DiscountCalculator : IDiscountCalculator
{
    private const decimal VipDiscountPercentage = 15m;
    private const decimal PremiumDiscountPercentage = 10m;
    
    public DiscountInfo CalculateDiscount(decimal amount, string userType)
    {
        var discountPercentage = GetDiscountPercentage(userType);
        var discountAmount = amount * discountPercentage / 100m;
        var finalAmount = amount - discountAmount;
        
        return new DiscountInfo
        {
            OriginalAmount = amount,
            DiscountPercentage = discountPercentage,
            DiscountAmount = discountAmount,
            FinalAmount = finalAmount
        };
    }
    
    private decimal GetDiscountPercentage(string userType)
    {
        return userType switch
        {
            "VIP" => VipDiscountPercentage,
            "Premium" => PremiumDiscountPercentage,
            _ => 0m
        };
    }
}

public class DiscountInfo
{
    public decimal OriginalAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalAmount { get; set; }
}

