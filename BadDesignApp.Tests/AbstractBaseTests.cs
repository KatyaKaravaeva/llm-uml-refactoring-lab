using BadDesignApp.Services;
using Xunit;

namespace BadDesignApp.Tests;

// Anx5: Бессмысленные тесты для абстрактного класса без наследников
// Невозможно создать экземпляр, но пишем тесты
public class AbstractBaseTests
{
    // Проблема: нельзя создать экземпляр абстрактного класса
    // Тесты не могут быть выполнены, но они написаны
    
    // Попытка тестировать несуществующий функционал
    [Fact]
    public void AbstractBase_ShouldHaveBaseField()
    {
        // Этот тест не может работать, так как класс абстрактный
        // и нет наследников для тестирования
        // Но тест написан для демонстрации проблемы
        
        // Попытка использовать рефлексию для доступа к внутренним полям
        var baseType = typeof(AbstractBase);
        var field = baseType.GetField("_baseField", 
            System.Reflection.BindingFlags.NonPublic | 
            System.Reflection.BindingFlags.Instance);
        
        Assert.NotNull(field);
    }
    
    [Fact]
    public void AbstractBase_MethodsShouldExist()
    {
        // Тест проверяет наличие методов, которые никогда не будут вызваны
        var baseType = typeof(AbstractBase);
        var doWorkMethod = baseType.GetMethod("DoWork");
        var getResultMethod = baseType.GetMethod("GetResult");
        
        Assert.NotNull(doWorkMethod);
        Assert.NotNull(getResultMethod);
        Assert.True(doWorkMethod.IsAbstract);
        Assert.True(getResultMethod.IsAbstract);
    }
    
    // Бесполезные тесты для класса, который не может быть использован
}


