using Godot;

namespace Movement;

[GlobalClass]
public partial class ChaseMoveMod : MoveMod {
    [Export] public float Speed { get; set; }

    public Node2D Source { get; set; }
    public Node2D Target { get; set; }

    public override MoveOutput Modify(float speed) {
        if (Target != null && IsInstanceValid(Target)) {
            var direction = Source.GlobalPosition.DirectionTo(Target.GlobalPosition);
            return new MoveOutput(direction * speed);
        }
        return MoveOutput.Silenced;
    }
}
