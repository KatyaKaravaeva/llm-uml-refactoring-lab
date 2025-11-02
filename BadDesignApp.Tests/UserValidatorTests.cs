using BadDesignApp.Services.Validation;
using Xunit;

namespace BadDesignApp.Tests;

// Устранено нарушение Anx1 - модульные тесты для валидатора
// Тесты проверяют только публичное поведение, не внутреннее состояние
public class UserValidatorTests
{
    [Fact]
    public void Validate_ValidUserId_ShouldReturnSuccess()
    {
        // Arrange
        var validator = new UserValidator();
        
        // Act
        var result = validator.Validate("user12345");
        
        // Assert
        Assert.True(result.IsValid);
        Assert.Null(result.ErrorMessage);
    }
    
    [Fact]
    public void Validate_EmptyUserId_ShouldReturnFailure()
    {
        // Arrange
        var validator = new UserValidator();
        
        // Act
        var result = validator.Validate("");
        
        // Assert
        Assert.False(result.IsValid);
        Assert.NotNull(result.ErrorMessage);
    }
    
    [Fact]
    public void Validate_ShortUserId_ShouldReturnFailure()
    {
        // Arrange
        var validator = new UserValidator();
        
        // Act
        var result = validator.Validate("abc");
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("at least", result.ErrorMessage ?? "");
    }
    
    [Fact]
    public void Validate_NullUserId_ShouldReturnFailure()
    {
        // Arrange
        var validator = new UserValidator();
        
        // Act
        var result = validator.Validate(null!);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.NotNull(result.ErrorMessage);
    }
}

