using Godot;
using Movement;

[GlobalClass]
public partial class Protostar : CharacterBody2D {
    [Export] Chase chase;

    public Node2D Target;

    public override void _Ready() {
        chase.Source = this;
        chase.Target = Target;
    }

    public override void _Process(double delta) {
        if (Target != null && IsInstanceValid(Target)) {
            LookAt(Target.GlobalPosition);
        }
    }
}
