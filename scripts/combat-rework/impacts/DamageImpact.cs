using Godot;

[GlobalClass]
public partial class DamageImpact : WeaponImpact {
    [Export] public float PowerToDamageRatio { get; private set; } = 1f;

    public override void Apply(ImpactContext impact, AttackContext attack) {
        if (impact.Victim.TryGetComponent<SimpleHealth>(out var health)) {
            var damage = attack.Power * PowerToDamageRatio;
            health.Reduce(damage);
        }
    }
}
