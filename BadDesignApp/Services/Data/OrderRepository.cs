namespace BadDesignApp.Services.Data;

// Устранено нарушение SRP - отдельный класс для работы с данными
public class OrderRepository : IOrderRepository
{
    private readonly List<OrderData> _orders = new();
    private int _transactionCount = 0;
    
    public void SaveOrder(OrderData orderData)
    {
        _transactionCount++;
        orderData.TransactionNumber = _transactionCount;
        orderData.ProcessedDate = DateTime.Now;
        _orders.Add(orderData);
    }
    
    public int GetTransactionCount()
    {
        return _transactionCount;
    }
    
    public IReadOnlyList<OrderData> GetAllOrders() => _orders.AsReadOnly();
}

