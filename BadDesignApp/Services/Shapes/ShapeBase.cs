namespace BadDesignApp.Services.Shapes;

// Устранено нарушение Anx3 - базовый класс для всех фигур с поддержкой цвета
// Единая иерархия вместо параллельных - добавление нового типа требует только одного класса
public abstract class ShapeBase : IShape
{
    public string? Color { get; set; }
    
    public abstract double GetArea();
    
    public virtual void Draw()
    {
        if (!string.IsNullOrEmpty(Color))
        {
            Console.WriteLine($"Drawing {Color} {GetShapeName()}");
        }
        else
        {
            Console.WriteLine($"Drawing {GetShapeName()}");
        }
    }
    
    protected abstract string GetShapeName();
}

