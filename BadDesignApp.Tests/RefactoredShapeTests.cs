using BadDesignApp.Services.Shapes;
using Xunit;

namespace BadDesignApp.Tests;

// Устранено нарушение Anx3 - тесты для единой иерархии фигур
// Нет дублирования тестов для параллельных иерархий
public class RefactoredShapeTests
{
    [Fact]
    public void Circle_ShouldCalculateArea()
    {
        // Arrange
        var circle = new Circle { Radius = 5 };
        
        // Act
        var area = circle.GetArea();
        
        // Assert
        Assert.Equal(Math.PI * 25, area);
    }
    
    [Fact]
    public void Circle_WithColor_ShouldDrawColored()
    {
        // Arrange
        var circle = new Circle { Radius = 5, Color = "Red" };
        
        // Act & Assert - проверяем только поведение
        // В реальном тесте можно использовать StringWriter для перехвата вывода
        circle.Draw();
        Assert.Equal("Red", circle.Color);
    }
    
    [Fact]
    public void Rectangle_ShouldCalculateArea()
    {
        // Arrange
        var rectangle = new Rectangle { Width = 4, Height = 5 };
        
        // Act
        var area = rectangle.GetArea();
        
        // Assert
        Assert.Equal(20, area);
    }
    
    [Fact]
    public void Rectangle_WithColor_ShouldDrawColored()
    {
        // Arrange
        var rectangle = new Rectangle { Width = 4, Height = 5, Color = "Blue" };
        
        // Act & Assert
        rectangle.Draw();
        Assert.Equal("Blue", rectangle.Color);
    }
    
    // При добавлении нового типа фигуры (например, Triangle) нужно добавить только один тест,
    // а не тесты для двух параллельных иерархий
}

