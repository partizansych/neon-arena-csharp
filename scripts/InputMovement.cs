using Godot;

[GlobalClass]
public partial class InputMovement : Node2D {
    [Export] VelocityDebugger debugger;

    public Vector2 Direction { get; private set; }

    public override void _Ready() {
        if (debugger != null) {
            debugger.MaxVelocityReference = 1f;
        }
    }

    public override void _PhysicsProcess(double delta) {
        Direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

        if (debugger != null)
            debugger.CurrentVelocity = Direction;
    }
}
