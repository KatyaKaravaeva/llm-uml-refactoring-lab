namespace BadDesignApp.Services.Data;

// Устранено нарушение Anx6 - правильная инкапсуляция через свойства
// Нет internal полей, доступ только через публичный интерфейс
public class RefactoredDataAccessor
{
    // Приватные поля с публичными свойствами для доступа
    private string _data = "default";
    private int _counter = 0;
    private bool _isInitialized = false;
    
    // Публичные свойства для безопасного доступа
    public string Data
    {
        get => _data;
        set
        {
            if (!string.IsNullOrEmpty(value))
                _data = value;
        }
    }
    
    public int Counter
    {
        get => _counter;
        private set => _counter = value;
    }
    
    public bool IsInitialized
    {
        get => _isInitialized;
        private set => _isInitialized = value;
    }
    
    // Публичные методы для работы с данными
    public void IncrementCounter()
    {
        Counter++;
    }
    
    public void Initialize()
    {
        if (!IsInitialized)
        {
            IsInitialized = true;
            Counter = 0;
        }
    }
    
    public void Reset()
    {
        _data = "default";
        Counter = 0;
        IsInitialized = false;
    }
}

