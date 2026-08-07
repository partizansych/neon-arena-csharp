using Godot;

[GlobalClass]
public partial class ChaseMovementModifier : MovementModifier {
    [Export] public float Speed { get; set; }

    public Node2D Source { get; set; }
    public Node2D Target { get; set; }

    public override void Modify(ref Vector2 velocity) {
        var direction = Source.GlobalPosition.DirectionTo(Target.GlobalPosition);
        velocity += direction * Speed;
    }
}
