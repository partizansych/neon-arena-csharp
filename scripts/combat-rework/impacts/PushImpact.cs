using Godot;
using Movement;

[GlobalClass]
public partial class PushImpact : WeaponImpact {
    [Export] public float Force { get; private set; }
    [Export] public float PowerToForceRatio { get; private set; } = 0f;

    public override void Apply(ImpactContext impact, AttackContext attack) {
        if (impact.Victim.TryGetComponent<Impulse>(out var component)) {
            var force = Force + attack.Power * PowerToForceRatio;
            component.ApplyImpulse(-impact.HitNormal * force);
        }
    }
}
