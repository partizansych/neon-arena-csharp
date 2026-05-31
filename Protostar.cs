using System;
using Godot;

[GlobalClass]
public partial class Protostar : CharacterBody2D, IHittable {
    [Export] SimpleHealth health;
    [Export] KnockbackComponent knockback;
    [Export] public float Speed = 150f;
    [Export] public float MaxHp = 100f;
    [Export] public AudioStream HitSFX;
    [Export] public AudioStream DeathSFX;

    public event Action Died;

    public Node2D Target;

    public override void _Ready() {
        health.Died += OnDied;
    }

    public override void _PhysicsProcess(double delta) {
        if (Target != null) {
            var myPos = GlobalPosition;
            var targetPos = Target.GlobalPosition;
            var direction = myPos.DirectionTo(targetPos);
            var chaseVelocity = direction * Speed;

            Velocity = chaseVelocity + knockback.Velocity;
            MoveAndSlide();

            // var collision = MoveAndCollide(Velocity);
            // if (collision != null) {
            //     var node = collision.GetCollider() as Node2D;
            //     if (node.IsInGroup("enemies")) {
            //         var knockDir = node.GlobalPosition.DirectionTo(myPos);
            //         knockback.Add(knockDir, 1f);
            //     }
            // }
        }
    }

    public override void _Process(double delta) {
        LookAt(Target.GlobalPosition);
    }

    public void TakeDamage(float amount) {
        health.Reduce(amount);
        if (HitSFX != null) {
            Audio.Instance.Play(HitSFX, Audio.BUS_SFX);
        }
    }

    public void Hit(HitInfo info) {
        if (info.Type == HitType.Damage) {
            TakeDamage(info.Value);
        }

        var direction = info.HitPoint.DirectionTo(GlobalPosition);
        knockback.Add(direction, info.KnockbackForce);
    }

    private void OnDied() {
        Died?.Invoke();
        QueueFree();
        if (DeathSFX != null) {
            Audio.Instance.Play(HitSFX, Audio.BUS_SFX);
        }
    }
}
