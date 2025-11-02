using System.Text;

namespace BadDesignApp.Services.Output;

// Устранено нарушение SRP - отдельный класс для вывода данных
public class ConsoleOutputService : IOutputService
{
    public void PrintReceipt(ReceiptData receiptData)
    {
        var receipt = new StringBuilder();
        receipt.AppendLine("=== RECEIPT ===");
        receipt.AppendLine($"User: {receiptData.UserId}");
        receipt.AppendLine($"Type: {receiptData.UserType}");
        receipt.AppendLine($"Amount: {receiptData.Amount:C}");
        receipt.AppendLine($"Discount: {receiptData.DiscountPercentage}%");
        Console.WriteLine(receipt.ToString());
    }
    
    public void PrintError(string error)
    {
        Console.WriteLine($"[ERROR] {error}");
    }
    
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}

