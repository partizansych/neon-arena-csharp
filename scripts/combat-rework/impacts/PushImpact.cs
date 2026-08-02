using Godot;

[GlobalClass]
public partial class PushImpact : WeaponImpact {
    [Export] public float Force { get; private set; }
    [Export] public float PowerToForceRatio { get; private set; } = 0f;

    public override void Apply(ImpactContext impact, AttackContext attack) {
        if (impact.Victim.TryGetComponent<KnockbackComponent>(out var knockback)) {
            var force = Force + attack.Power * PowerToForceRatio;
            knockback.Add(-impact.HitNormal, force);
        }
    }
}
