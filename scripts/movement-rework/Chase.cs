namespace Movement;

using Godot;

[GlobalClass]
public partial class Chase : MoveMod {
    [Export] public float Speed { get; set; }

    public Node2D Source { get; set; }
    public Node2D Target { get; set; }

    public override Vector2 Modify(Vector2 vel) {
        if (Target != null && IsInstanceValid(Target)) {
            var direction = Source.GlobalPosition.DirectionTo(Target.GlobalPosition);
            return direction * Speed;
        }
        return vel;
    }
}
