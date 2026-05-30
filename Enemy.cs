using Godot;

[GlobalClass]
public partial class Enemy : Character {
    public Node2D Target;

    public override void _PhysicsProcess(double delta) {
        if (Target != null) {
            var targetPos = Target.GlobalPosition;
            var dirToTarget = GlobalPosition.DirectionTo(targetPos);

            Velocity = dirToTarget * Get(CharacterStat.Speed);

            LookAt(targetPos);
            MoveAndSlide();
        }
    }
}
