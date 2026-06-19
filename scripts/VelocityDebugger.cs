using Godot;

[GlobalClass]
public partial class VelocityDebugger : Node2D {
    [Export] public bool Enabled = true;
    [Export] public Color Color = Colors.Green;
    [Export] public float CircleRadius = 2.5f;
    [Export] public float MaxLineLength = 100f;
    [Export] public float MaxVelocityReference = 400f;
    [Export] public int FontSize = 14;
    [Export] public Vector2 FontOffset = new(8, -5);

    // Ссылка на вектор, который нужно отрисовывать
    public Vector2 CurrentVelocity { get; set; }

    public override void _PhysicsProcess(double delta) {
        if (Enabled) {
            QueueRedraw();
        }
    }

    public override void _Draw() {
        if (!Enabled) return;

        // Сбрасываем глобальную трансформацию, чтобы рисовать относительно мировых координат,
        // но привязавшись к позиции родителя
        DrawSetTransformMatrix(GlobalTransform.AffineInverse());

        var scaledVector = CurrentVelocity * (MaxLineLength / MaxVelocityReference);
        var start = GlobalPosition;
        var end = start + scaledVector;

        DrawLine(start, end, Color);
        DrawCircle(end, CircleRadius, Color);

        var font = ThemeDB.FallbackFont;
        var text = Mathf.Round(CurrentVelocity.Length()).ToString();
        DrawString(font, end + FontOffset, text, fontSize: FontSize);
    }
}
