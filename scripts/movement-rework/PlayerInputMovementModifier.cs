using Godot;

[GlobalClass]
public partial class PlayerInputMovementModifier : MovementModifier {
    [Export] DashMovementModifier dash;

    public Vector2 InputVector { get; private set; }

    public override void _PhysicsProcess(double delta) {
        InputVector = Input.GetVector("move_left", "move_right", "move_up", "move_down");

        dash.SetSteeringDirection(InputVector);
    }

    public override void _UnhandledInput(InputEvent @event) {
        if (@event.IsActionPressed("dash") && InputVector != Vector2.Zero) {
            dash.Start(InputVector);
        }
    }

    public override void Modify(ref Vector2 velocity) {
        velocity += InputVector * 200f;
    }
}
