using Godot;

[GlobalClass]
public abstract partial class WeaponImpact : Resource, IImpactEffect {
    public abstract void Apply(ImpactContext impact, AttackContext attack);
}
