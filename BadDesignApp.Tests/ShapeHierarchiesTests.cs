using BadDesignApp.Services;
using Xunit;

namespace BadDesignApp.Tests;

// Anx3: Тесты для параллельных иерархий
// При добавлении нового типа нужно обновлять тесты в обеих иерархиях
public class ShapeHierarchiesTests
{
    // Тесты для первой иерархии
    [Fact]
    public void Circle_ShouldCalculateArea()
    {
        var circle = new Circle { Radius = 5 };
        var area = circle.GetArea();
        Assert.Equal(Math.PI * 25, area);
    }
    
    [Fact]
    public void Rectangle_ShouldCalculateArea()
    {
        var rectangle = new Rectangle { Width = 4, Height = 5 };
        var area = rectangle.GetArea();
        Assert.Equal(20, area);
    }
    
    // Тесты для второй параллельной иерархии
    [Fact]
    public void ColoredCircle_ShouldCalculateArea()
    {
        // Дублирование логики из первой иерархии
        var circle = new ColoredCircle { Radius = 5, Color = "Red" };
        var area = circle.GetArea();
        Assert.Equal(Math.PI * 25, area);
    }
    
    [Fact]
    public void ColoredRectangle_ShouldCalculateArea()
    {
        // Дублирование логики из первой иерархии
        var rectangle = new ColoredRectangle { Width = 4, Height = 5, Color = "Blue" };
        var area = rectangle.GetArea();
        Assert.Equal(20, area);
    }
    
    // Anx3: При добавлении Triangle нужно:
    // 1. Добавить тест для Triangle
    // 2. Добавить тест для ColoredTriangle
    // 3. Обновить все существующие тесты при изменении базовой логики
}


