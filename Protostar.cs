using System;
using Godot;

[GlobalClass]
public partial class Protostar : CharacterBody2D, IHittable {
    [Export] SimpleHealth health;
    [Export] KnockbackHandler knockback;
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
            var targetPos = Target.GlobalPosition;
            var direction = GlobalPosition.DirectionTo(targetPos);
            var chaseVelocity = direction * Speed;

            Velocity = chaseVelocity + knockback.Velocity;

            MoveAndSlide();
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
