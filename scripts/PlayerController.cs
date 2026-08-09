using Godot;
using Movement;

[GlobalClass]
public partial class PlayerController : Node {
    [Export] InputMoveMod input;
    [Export] DashMoveMod dash;

    public override void _PhysicsProcess(double delta) {
        dash.SetSteeringDirection(input.Vector);
    }

    public override void _UnhandledInput(InputEvent @event) {
        if (@event.IsActionPressed("dash") && input.Vector != Vector2.Zero) {
            dash.Start(input.Vector);
        }
    }
}
