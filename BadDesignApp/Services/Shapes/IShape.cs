namespace BadDesignApp.Services.Shapes;

// Устранено нарушение Anx3 - единый интерфейс для всех фигур
// Использован принцип композиции вместо параллельных иерархий
public interface IShape
{
    double GetArea();
    void Draw();
    string? Color { get; }
}

