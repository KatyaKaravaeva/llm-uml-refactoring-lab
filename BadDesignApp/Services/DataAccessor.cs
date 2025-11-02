namespace BadDesignApp.Services;

// Anx6: Нарушение инкапсуляции - прямой доступ к закрытым полям
public class DataAccessor
{
    // Internal поля для обхода инкапсуляции
    internal string _privateData;
    internal int _privateCounter;
    internal bool _isInitialized;
    
    public DataAccessor()
    {
        _privateData = "default";
        _privateCounter = 0;
        _isInitialized = false;
    }
    
    // Публичный метод для доступа к internal полю (плохая практика)
    public void SetPrivateDataDirectly(string data)
    {
        _privateData = data;
    }
    
    // Публичный метод для доступа к internal полю
    public string GetPrivateDataDirectly()
    {
        return _privateData;
    }
    
    // Нормальный публичный интерфейс (игнорируется в тестах)
    public void SetData(string data)
    {
        if (!string.IsNullOrEmpty(data))
            _privateData = data;
    }
    
    public string GetData()
    {
        return _privateData;
    }
}


