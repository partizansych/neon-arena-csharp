using Godot;

[GlobalClass]
public partial class KnockbackComponent : Node2D {
    // Чем выше, тем резче остановка. 5-15 = "crunchy" импульс, 1-3 = плавное скольжение
    [Export] public float Damping = 10f;
    [Export] public float Max = 600f;

    [ExportGroup("Debug")]
    [Export] public bool Debug;
    [Export] public Color DebugColor = Colors.Green;
    [Export] public float DebugCircleRadius = 2.5f;
    [Export] public float DebugMaxLineLength = 100f;
    [Export] public int DebugFontSize = 14;
    [Export] public Vector2 DebugFontOffset = new(8, -5);

    public Vector2 Velocity { get; private set; }

    public override void _PhysicsProcess(double delta) {
        if (!Velocity.IsZeroApprox()) {
            // Velocity = Velocity.Lerp(Vector2.Zero, 1f - Mathf.Exp(-Damping * (float)delta));
            Velocity *= Mathf.Max(0f, 1f - Damping * (float)delta);
        }

        if (Debug)
            QueueRedraw();
    }

    public override void _Draw() {
        DrawSetTransformMatrix(GlobalTransform.AffineInverse());
        var scaledVector = Velocity * (DebugMaxLineLength / 400f);
        var start = GlobalPosition;
        var end = start + scaledVector;
        DrawLine(start, end, DebugColor);
        DrawCircle(end, DebugCircleRadius, DebugColor);
        var font = ThemeDB.FallbackFont;
        var text = Mathf.Round(Velocity.Length()).ToString();
        DrawString(font, end + DebugFontOffset, text, fontSize: DebugFontSize);
    }

    public void Add(Vector2 direction, float force) {
        if (direction != Vector2.Zero && force > 0f) {
            Velocity += direction.Normalized() * force;
            if (Velocity.Length() > Max) {
                Velocity = Velocity.Normalized() * Max;
            }
        }
    }
}
