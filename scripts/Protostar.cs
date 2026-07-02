using Godot;

[GlobalClass]
public partial class Protostar : CharacterBody2D, IHittable {
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

    public void Hit(HitInfo info) {
        if (info.Type == HitType.Damage) {
            health.Reduce(info.Value);
            Event.Instance.Damaged.Invoke(info.Value, GlobalPosition);
        }

        var direction = info.HitPoint.DirectionTo(GlobalPosition);
        knockback.Add(direction, info.KnockbackForce);
    }
}
