namespace BadDesignApp.Services.Output;

// Устранено нарушение SRP - интерфейс для вывода данных
public interface IOutputService
{
    void PrintReceipt(ReceiptData receiptData);
    void PrintError(string error);
    void Log(string message);
}

public class ReceiptData
{
    public string UserId { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal DiscountPercentage { get; set; }
}

