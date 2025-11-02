namespace BadDesignApp.Services.Shapes;

// Устранено нарушение Anx3 - единая иерархия, нет дублирования с ColoredCircle
public class Circle : ShapeBase
{
    public double Radius { get; set; }
    
    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    protected override string GetShapeName() => $"circle with radius {Radius}";
}

