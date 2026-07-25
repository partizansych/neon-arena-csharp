using Godot;

[GlobalClass]
public partial class Protostar : CharacterBody2D {
    [Export] SimpleHealth health;
    [Export] KnockbackComponent knockback;
    [Export] public float Speed = 150f;
    [Export] public float MaxHp = 100f;

    public Node2D Target;

    public override void _PhysicsProcess(double delta) {
        if (Target != null) {
            var myPos = GlobalPosition;
            var targetPos = Target.GlobalPosition;
            var direction = myPos.DirectionTo(targetPos);
            var chaseVelocity = direction * Speed;

            Velocity = chaseVelocity + knockback.Velocity;
            // Velocity *= (float)delta;
            MoveAndSlide();
        }
    }

    public override void _Process(double delta) {
        if (Target != null) {
            LookAt(Target.GlobalPosition);
        }
    }
}
