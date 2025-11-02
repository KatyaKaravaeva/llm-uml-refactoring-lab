using BadDesignApp.Services.Data;
using Xunit;

namespace BadDesignApp.Tests;

// Устранено нарушение Anx6 - тесты используют только публичный интерфейс
// Нет обращения к internal полям через рефлексию
public class RefactoredDataAccessorTests
{
    [Fact]
    public void Data_Setter_WithValidValue_ShouldUpdateData()
    {
        // Arrange
        var accessor = new RefactoredDataAccessor();
        
        // Act
        accessor.Data = "test data";
        
        // Assert - проверка через публичное свойство
        Assert.Equal("test data", accessor.Data);
    }
    
    [Fact]
    public void Data_Setter_WithEmptyValue_ShouldNotUpdateData()
    {
        // Arrange
        var accessor = new RefactoredDataAccessor();
        accessor.Data = "initial";
        
        // Act
        accessor.Data = "";
        
        // Assert - проверка, что значение не изменилось
        Assert.Equal("initial", accessor.Data);
    }
    
    [Fact]
    public void IncrementCounter_ShouldIncreaseCounter()
    {
        // Arrange
        var accessor = new RefactoredDataAccessor();
        var initialCount = accessor.Counter;
        
        // Act
        accessor.IncrementCounter();
        
        // Assert - проверка через публичное свойство
        Assert.Equal(initialCount + 1, accessor.Counter);
    }
    
    [Fact]
    public void Initialize_FirstTime_ShouldSetIsInitialized()
    {
        // Arrange
        var accessor = new RefactoredDataAccessor();
        
        // Act
        accessor.Initialize();
        
        // Assert - проверка через публичное свойство
        Assert.True(accessor.IsInitialized);
        Assert.Equal(0, accessor.Counter);
    }
    
    [Fact]
    public void Reset_ShouldResetAllFields()
    {
        // Arrange
        var accessor = new RefactoredDataAccessor();
        accessor.Data = "test";
        accessor.IncrementCounter();
        accessor.Initialize();
        
        // Act
        accessor.Reset();
        
        // Assert - проверка через публичные свойства
        Assert.Equal("default", accessor.Data);
        Assert.Equal(0, accessor.Counter);
        Assert.False(accessor.IsInitialized);
    }
}

