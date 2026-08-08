using Godot;

[GlobalClass]
public abstract partial class WeaponImpact : Resource {
    public abstract void Apply(ImpactContext impact, AttackContext attack);
}
