using Godot;

[GlobalClass]
public partial class Eye : Node2D {
    [Export] Node2D pupil;
    [Export] public float MaxOffset = 15f;
    [Export] public float MaxCursorDistance = 200f;

    public override void _Ready() {
        if (pupil == null) {
            GD.PushError($"Ссылка на зрачок не установлена.");
            return;
        }
    }

    public override void _PhysicsProcess(double delta) {
        if (pupil == null) return;

        var globalMousePos = GetGlobalMousePosition();
        var toMouse = globalMousePos - GlobalPosition;
        var currentDistance = toMouse.Length();

        if (currentDistance == 0) {
            pupil.Position = Vector2.Zero;
            return;
        }

        var direction = toMouse.Normalized();
        var pupilOffsetLength = Mathf.Remap(currentDistance, 0, MaxCursorDistance, 0, MaxOffset);
        pupilOffsetLength = Mathf.Clamp(pupilOffsetLength, 0, MaxOffset);
        pupil.Position = direction * pupilOffsetLength;
    }
}
