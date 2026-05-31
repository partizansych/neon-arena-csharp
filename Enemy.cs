using Godot;

[GlobalClass]
public partial class Enemy : Character {
    public Node2D Target;

    public override void _Ready() {

    }

    public override void _PhysicsProcess(double delta) {
        if (Target != null) {
            var targetPos = Target.GlobalPosition;
            var direction = GlobalPosition.DirectionTo(targetPos);
            var chaseVelocity = direction * Get(CharacterStat.Speed);

            UpdateKnockback((float)delta);
            Velocity = chaseVelocity + Knockback;
            MoveAndSlide();
            LookAt(targetPos);
        }
    }


}
