using Godot;

[GlobalClass]
public partial class Rotator2D : Node {
    [Export] public Node2D Target { get; set; }
    [Export] public float Speed { get; set; } = 0.25f; // Радианы

    public override void _Ready() {
        Target ??= GetParent<Node2D>();
    }

    public override void _Process(double delta) {
        if (Target == null) return;

        float rotationAmount = Speed * (float)delta;
        Target.Rotate(rotationAmount);
    }
}
