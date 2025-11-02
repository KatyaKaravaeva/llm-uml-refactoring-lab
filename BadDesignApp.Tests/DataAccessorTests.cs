using BadDesignApp.Services;
using Xunit;

namespace BadDesignApp.Tests;

// Anx6: Тесты напрямую обращаются к внутренним полям через internal
// Нарушение инкапсуляции - тесты не используют публичный интерфейс
public class DataAccessorTests
{
    [Fact]
    public void SetPrivateDataDirectly_ShouldModifyInternalField()
    {
        // Тест использует обходной метод для доступа к internal полю
        var accessor = new DataAccessor();
        accessor.SetPrivateDataDirectly("test data");
        
        // Прямой доступ к internal полю через обходной метод
        var data = accessor.GetPrivateDataDirectly();
        Assert.Equal("test data", data);
    }
    
    [Fact]
    public void InternalFields_ShouldBeAccessible()
    {
        // Тест напрямую обращается к internal полям через рефлексию
        var accessor = new DataAccessor();
        
        // Использование рефлексии для доступа к private/internal полям
        var fieldType = typeof(DataAccessor);
        var privateDataField = fieldType.GetField("_privateData",
            System.Reflection.BindingFlags.NonPublic | 
            System.Reflection.BindingFlags.Instance);
        
        Assert.NotNull(privateDataField);
        
        // Прямое изменение internal поля
        privateDataField.SetValue(accessor, "reflection data");
        
        // Проверка через обходной метод (не через нормальный интерфейс)
        var value = accessor.GetPrivateDataDirectly();
        Assert.Equal("reflection data", value);
    }
    
    [Fact]
    public void InternalCounter_ShouldBeAccessible()
    {
        // Тест обращается к internal полю через рефлексию
        var accessor = new DataAccessor();
        var fieldType = typeof(DataAccessor);
        var counterField = fieldType.GetField("_privateCounter",
            System.Reflection.BindingFlags.NonPublic | 
            System.Reflection.BindingFlags.Instance);
        
        // Прямое чтение internal поля
        var counter = counterField.GetValue(accessor);
        Assert.Equal(0, counter);
        
        // Прямое изменение internal поля
        counterField.SetValue(accessor, 42);
        counter = counterField.GetValue(accessor);
        Assert.Equal(42, counter);
    }
    
    [Fact]
    public void InternalIsInitialized_ShouldBeAccessible()
    {
        // Тест проверяет internal поле вместо использования публичного API
        var accessor = new DataAccessor();
        var fieldType = typeof(DataAccessor);
        var initializedField = fieldType.GetField("_isInitialized",
            System.Reflection.BindingFlags.NonPublic | 
            System.Reflection.BindingFlags.Instance);
        
        var isInitialized = initializedField.GetValue(accessor);
        Assert.False((bool)isInitialized);
        
        // Прямое изменение internal поля
        initializedField.SetValue(accessor, true);
        isInitialized = initializedField.GetValue(accessor);
        Assert.True((bool)isInitialized);
    }
    
    // Тест игнорирует нормальный публичный интерфейс GetData/SetData
    // и использует обходные методы или рефлексию
}


