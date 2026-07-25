using Godot;

[GlobalClass]
public partial class StraightMovement2D : Node {
    [Export] public Node2D Target { get; set; }

    [Export] public Vector2 Direction { get; set; } = Vector2.Right;
    [Export] public float Speed { get; set; } = 10f;

    public override void _Ready() {
        Target ??= GetParent<Node2D>();
    }

    public override void _PhysicsProcess(double delta) {
        if (Target != null && Direction != Vector2.Zero) {
            Target.GlobalPosition += Direction * Speed * (float)delta;
        }
    }
}
