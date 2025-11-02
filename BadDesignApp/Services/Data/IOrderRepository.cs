namespace BadDesignApp.Services.Data;

// Устранено нарушение SRP - интерфейс для работы с данными заказов
public interface IOrderRepository
{
    void SaveOrder(OrderData orderData);
    int GetTransactionCount();
}

public class OrderData
{
    public string UserId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime ProcessedDate { get; set; }
    public int TransactionNumber { get; set; }
}

