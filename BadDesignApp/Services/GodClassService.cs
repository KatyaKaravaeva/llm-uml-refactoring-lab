using System.Text;

namespace BadDesignApp.Services;

// Anx1: Божественный класс с множеством ответственностей
// Нарушение SRP - класс выполняет валидацию, бизнес-логику и вывод данных
public class GodClassService
{
    // Множество атрибутов - признак GodClass
    private string _currentUserId;
    private decimal _totalAmount;
    private string _userType;
    private List<string> _orderHistory;
    private Dictionary<string, object> _cache;
    private int _transactionCount;
    private DateTime _lastProcessedDate;
    private bool _isValidated;
    private string _lastError;
    private int _discountPercentage;
    
    public GodClassService()
    {
        _orderHistory = new List<string>();
        _cache = new Dictionary<string, object>();
        _transactionCount = 0;
        _isValidated = false;
    }
    
    // Бизнес-логика
    public void ProcessUserOrder(string userId, decimal amount, string userType)
    {
        _currentUserId = userId;
        _totalAmount = amount;
        _userType = userType;
        
        // Валидация внутри бизнес-логики
        if (!ValidateUser(userId))
        {
            _lastError = "Invalid user";
            PrintError(_lastError);
            return;
        }
        
        // Вычисление скидки
        CalculateDiscount();
        
        // Обработка заказа
        ProcessOrder();
        
        // Логирование
        LogTransaction();
        
        // Вывод данных
        PrintReceipt();
    }
    
    // Валидация (должна быть отдельным классом)
    private bool ValidateUser(string userId)
    {
        if (string.IsNullOrEmpty(userId) || userId.Length < 5)
        {
            _isValidated = false;
            return false;
        }
        _isValidated = true;
        return true;
    }
    
    // Бизнес-логика (должна быть отдельным классом)
    private void CalculateDiscount()
    {
        if (_userType == "VIP")
            _discountPercentage = 15;
        else if (_userType == "Premium")
            _discountPercentage = 10;
        else
            _discountPercentage = 0;
        
        _totalAmount = _totalAmount * (1 - _discountPercentage / 100m);
    }
    
    private void ProcessOrder()
    {
        _transactionCount++;
        _lastProcessedDate = DateTime.Now;
        _orderHistory.Add($"Order {_transactionCount}: {_totalAmount:C}");
        _cache[$"order_{_transactionCount}"] = new { UserId = _currentUserId, Amount = _totalAmount };
    }
    
    // Логирование (должно быть отдельным классом)
    private void LogTransaction()
    {
        // Эмуляция логирования
        var logMessage = $"{DateTime.Now}: User {_currentUserId} processed order {_transactionCount}";
        Console.WriteLine($"[LOG] {logMessage}");
    }
    
    // Вывод данных (должен быть отдельным классом)
    private void PrintReceipt()
    {
        var receipt = new StringBuilder();
        receipt.AppendLine("=== RECEIPT ===");
        receipt.AppendLine($"User: {_currentUserId}");
        receipt.AppendLine($"Type: {_userType}");
        receipt.AppendLine($"Amount: {_totalAmount:C}");
        receipt.AppendLine($"Discount: {_discountPercentage}%");
        Console.WriteLine(receipt.ToString());
    }
    
    private void PrintError(string error)
    {
        Console.WriteLine($"[ERROR] {error}");
    }
    
    // Множество геттеров для доступа к внутреннему состоянию
    public string GetCurrentUserId() => _currentUserId;
    public decimal GetTotalAmount() => _totalAmount;
    public int GetTransactionCount() => _transactionCount;
    public bool IsValidated() => _isValidated;
    public string GetLastError() => _lastError;
}


