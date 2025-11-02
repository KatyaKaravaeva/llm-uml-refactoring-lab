namespace BadDesignApp.Services;

// Anx5: Бесполезный абстрактный класс без реальных наследников
public abstract class AbstractBase
{
    protected string _baseField;
    
    public AbstractBase()
    {
        _baseField = "base";
    }
    
    // Абстрактные методы без реализации
    public abstract void DoWork();
    public abstract string GetResult();
    
    // Методы, которые никогда не используются, так как нет наследников
    protected virtual void Initialize()
    {
        _baseField = "initialized";
    }
    
    public virtual void Cleanup()
    {
        _baseField = "cleaned";
    }
    
    // Публичные методы для тестирования, хотя класс бесполезен
    public string GetBaseField() => _baseField;
}

// Нет реальных наследников - класс бесполезен


