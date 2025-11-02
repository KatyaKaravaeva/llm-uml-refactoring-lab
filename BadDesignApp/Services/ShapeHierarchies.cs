namespace BadDesignApp.Services;

// Anx3: Параллельные иерархии - две схожие иерархии классов
// Добавление нового подкласса требует изменений в обеих иерархиях

// Первая иерархия - геометрические фигуры
public abstract class Shape
{
    public abstract double GetArea();
    public abstract void Draw();
}

public class Circle : Shape
{
    public double Radius { get; set; }
    
    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public override void Draw()
    {
        Console.WriteLine($"Drawing circle with radius {Radius}");
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public override double GetArea()
    {
        return Width * Height;
    }
    
    public override void Draw()
    {
        Console.WriteLine($"Drawing rectangle {Width}x{Height}");
    }
}

// Вторая параллельная иерархия - цветные фигуры
public abstract class ColoredShape
{
    public string Color { get; set; }
    public abstract double GetArea();
    public abstract void Paint();
}

public class ColoredCircle : ColoredShape
{
    public double Radius { get; set; }
    
    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public override void Paint()
    {
        Console.WriteLine($"Painting {Color} circle with radius {Radius}");
    }
}

public class ColoredRectangle : ColoredShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public override double GetArea()
    {
        return Width * Height;
    }
    
    public override void Paint()
    {
        Console.WriteLine($"Painting {Color} rectangle {Width}x{Height}");
    }
}

// При добавлении нового типа фигуры (например, Triangle) нужно:
// 1. Добавить Triangle : Shape
// 2. Добавить ColoredTriangle : ColoredShape
// 3. Обновить все тесты в обеих иерархиях


