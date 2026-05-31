using Godot;

[GlobalClass]
public partial class InputMovement : Node2D {
    [Export] public float Speed = 300f;

    [ExportGroup("Debug")]
    [Export] public bool Debug;
    [Export] public Color DebugColor = Colors.Blue;
    [Export] public float DebugCircleRadius = 2.5f;
    [Export] public float DebugMaxLineLength = 100f;
    [Export] public int DebugFontSize = 14;
    [Export] public Vector2 DebugFontOffset = new(8, -5);

    public Vector2 Velocity { get; private set; }

    public override void _PhysicsProcess(double delta) {
        var input = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = input * Speed;

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
}
