namespace BadDesignApp.Services.Shapes;

// Устранено нарушение Anx3 - единая иерархия, нет дублирования с ColoredRectangle
public class Rectangle : ShapeBase
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public override double GetArea()
    {
        return Width * Height;
    }
    
    protected override string GetShapeName() => $"rectangle {Width}x{Height}";
}

