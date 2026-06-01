using Godot;

[GlobalClass]
public partial class InputMovement : Node2D {
    [Export] public float Speed = 300f;
    [Export] VelocityDebugger debugger;

    public Vector2 Velocity { get; private set; }

    public override void _PhysicsProcess(double delta) {
        var input = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = input * Speed;

        if (debugger != null)
            debugger.CurrentVelocity = Velocity;
    }
}
